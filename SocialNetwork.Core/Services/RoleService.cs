using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Infrastructure;
using SocialNetwork.Dal.Interfaces;
using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Core.Services
{
    public class RoleService: IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<Dal.ORM.Role> roleRepository;
        public RoleService(IUnitOfWork uow, IRepository<Dal.ORM.Role> repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }

        #region Public Methods
        public void DeleteEntity(Role role)
        {
            roleRepository.Delete(role.ToDalRole());
            uow.Commit();
        }
        public IEnumerable<Role> GetAllEntities()
        {
            return roleRepository.GetAll().Select(role => role.ToBllRole());
        }
        public void CreateEntity(Role role)
        {
            roleRepository.Create(role.ToDalRole());
            uow.Commit();
        }
        public void UpdateEntity(Role role)
        {
            roleRepository.Update(role.ToDalRole());
            uow.Commit();
        }

        public Role FindByName(string name)
        {
            return roleRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<Dal.ORM.Role, string>(nameof(Dal.ORM.Role.Name), name)).ToBllRole();
        }
        #endregion
    }
}
