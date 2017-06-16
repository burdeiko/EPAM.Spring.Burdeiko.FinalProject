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
            return userRepository.GetById(id).ToBllUser();
        }
        public User GetUserByEMail(string eMail)
        {
            var users = userRepository.GetByPredicate(ByEMail(eMail));
            if (users == null || users.Count() == 0)
                return null;
            return users.First().ToBllUser();
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
        public void Update(User user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }
        #endregion
        public Expression<Func<Dal.ORM.User, bool>> ByEMail(string eMail)
        {
            return SearchExpressionBuilder.ByProperty<Dal.ORM.User, string>(nameof(Dal.ORM.User.E_Mail), eMail);
        }
        public void UpdateEntity(User user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }
    }
}
