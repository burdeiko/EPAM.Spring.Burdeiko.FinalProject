using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Infrastructure;
using SocialNetwork.Dal.Interfaces;
using SocialNetwork.Core.Interfaces;
using System.Linq.Expressions;

namespace SocialNetwork.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<Dal.ORM.Person> personRepository;
        public PersonService(IUnitOfWork uow, IRepository<Dal.ORM.Person> repository)
        {
            this.uow = uow;
            this.personRepository = repository;
        }

        #region Public Methods
        public void DeleteEntity(Person person)
        {
            personRepository.Delete(person.ToDalPerson());
            uow.Commit();
        }
        public IEnumerable<Person> GetAllEntities()
        {
            return personRepository.GetAll().Select(person => person.ToBllPerson());
        }
        public void CreateEntity(Person person)
        {
            personRepository.Create(person.ToDalPerson());
            uow.Commit();
        }
        public void UpdateEntity(Person person)
        {
            personRepository.Update(person.ToDalPerson());
            uow.Commit();
        }

        public Person FindByFirstName(string firstName)
        {
            var searchExpression = SearchExpressionBuilder.ByProperty<Dal.ORM.Person, string>(nameof(Dal.ORM.Person.FirstName), firstName);
            return personRepository.GetByPredicate(searchExpression).ToBllPerson();
        }

        public Person GetById(int id)
        {
            return personRepository.GetById(id).ToBllPerson();
        }


        #endregion
    }
}
