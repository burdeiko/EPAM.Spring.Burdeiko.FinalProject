﻿using System;
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
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IRepository<Dal.ORM.Role> roleRepository;
        #endregion
        #region Constructor
        public RoleService(IUnitOfWork uow, IRepository<Dal.ORM.Role> repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }
        #endregion
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
            return roleRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<Dal.ORM.Role, string>(nameof(Dal.ORM.Role.Name), name)).First().ToBllRole();
        }

        public Role GetRole(int id)
        {
            return roleRepository.GetById(id).ToBllRole();
        }
        #endregion
    }
}
