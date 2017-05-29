using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SocialNetwork.Core.Infrastructure;
using SocialNetwork.Dal.Interfaces;
using SocialNetwork.Core.Interfaces;


namespace SocialNetwork.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<Dal.ORM.User> userRepository;
        public UserService(IUnitOfWork uow, IRepository<Dal.ORM.User> repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }

        #region Public Methods

        public User GetUser(int id)
        {
            ParameterExpression parameterExp = Expression.Parameter(typeof(Dal.ORM.User));
            MemberExpression parameterId = Expression.Property(parameterExp, typeof(Dal.ORM.User).GetProperty("Id"));
            ConstantExpression idToSearch = Expression.Constant(id, typeof(int));
            BinaryExpression equalityExp = Expression.Equal(parameterId, idToSearch);
            Expression<Func<Dal.ORM.User, bool>> predicateExpression = Expression.Lambda<Func<Dal.ORM.User, bool>>(equalityExp, parameterExp);
            return userRepository.GetByPredicate(predicateExpression).ToBllUser();
        }
        public void DeleteEntity(User user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }
        public IEnumerable<User> GetAllEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }
        public void CreateEntity(User user)
        {
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }
        #endregion
        #region Search Expressions Getters
        public Expression<Func<Dal.ORM.User, bool>> ByEMail(string eMail)
        {
            return ByProperty(EMailPropertyName, eMail);
        }
        public Expression<Func<Dal.ORM.User, bool>> ByRoleId(int roleId)
        {
            return ByProperty(EMailPropertyName, roleId);
        }
        private Expression<Func<Dal.ORM.User, bool>> ByProperty<TProperty>(string propertyName, TProperty propertyValue)
        {
            if (typeof(Dal.ORM.User).GetProperty(propertyName) == null)
                throw new ArgumentException();
            ParameterExpression parameterExp = Expression.Parameter(typeof(Dal.ORM.User));
            MemberExpression parameterId = Expression.Property(parameterExp, typeof(Dal.ORM.User).GetProperty(propertyName));
            ConstantExpression idToSearch = Expression.Constant(propertyValue, typeof(TProperty));
            BinaryExpression equalityExp = Expression.Equal(parameterId, idToSearch);
            Expression<Func<Dal.ORM.User, bool>> predicateExpression = Expression.Lambda<Func<Dal.ORM.User, bool>>(equalityExp, parameterExp);
            return predicateExpression;
        }
        private readonly string EMailPropertyName = "E_Mail";
        private readonly string RolePropertyName = "RoleId";
        #endregion
    }
}
