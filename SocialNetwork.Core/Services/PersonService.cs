using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Infrastructure;
using SocialNetwork.Dal.Interfaces;
using SocialNetwork.Dal.ORM;
using SocialNetwork.Core.Interfaces;
using System.Linq.Expressions;

namespace SocialNetwork.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<Dal.ORM.Person> personRepository;
        private readonly IFriendRequestRepository friendRepository;
        public PersonService(IUnitOfWork uow, IRepository<Dal.ORM.Person> repository, IFriendRequestRepository friendRepository)
        {
            this.uow = uow;
            this.personRepository = repository;
            this.friendRepository = friendRepository;
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

        public IEnumerable<Person> FindByFirstName(string firstName)
        {
            var searchExpression = SearchExpressionBuilder.ByProperty<Dal.ORM.Person, string>(nameof(Dal.ORM.Person.FirstName), firstName);
            return personRepository.GetByPredicate(searchExpression).Select(m => m.ToBllPerson());
        }

        public Person GetById(int id)
        {
            return personRepository.GetById(id).ToBllPerson();
        }

        public IEnumerable<Person> GetFriendRequestSenders(int personId)
        {
            var requests = friendRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<FriendRequest, int>(nameof(FriendRequest.ReceiverId), personId));
            return requests.Where(m => m.IsConfirmed == false).Select(m => m.Sender.ToBllPerson());
        }

        public IEnumerable<Person> GetFriendRequestReceivers(int personId)
        {
            var requests = friendRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<FriendRequest, int>(nameof(FriendRequest.SenderId), personId));
            return requests.Where(m => m.IsConfirmed == false).Select(m => m.Receiver.ToBllPerson());
        }

        public IEnumerable<Person> GetFriends(int personId)
        {
            var outcomingRequests = friendRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<FriendRequest, int>(nameof(FriendRequest.SenderId), personId)).Where(m => m.IsConfirmed == true);
            var result = outcomingRequests.Select(m => m.Receiver);
            var incomingRequests = friendRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<FriendRequest, int>(nameof(FriendRequest.ReceiverId), personId)).Where(m => m.IsConfirmed == true);
            result = result.Union(incomingRequests.Select(m => m.Sender));
            return result.Select(m => m.ToBllPerson());
        }

        public void SendFriendRequest(int senderId, int receiverId)
        {
            friendRepository.Create(new FriendRequest { SenderId = senderId, ReceiverId = receiverId });
            uow.Commit();
        }

        public void AcceptFriendRequest(int senderId, int receiverId)
        {
            var request = friendRepository.GetById(senderId, receiverId);
            //if (request == null)
            //    throw new ArgumentException();
            request.IsConfirmed = true;
            friendRepository.Update(request);
            uow.Commit();
        }

        public IEnumerable<Person> FindByLastName(string lastName)
        {
            var searchExpression = SearchExpressionBuilder.ByProperty<Dal.ORM.Person, string>(nameof(Dal.ORM.Person.FirstName), lastName);
            return personRepository.GetByPredicate(searchExpression).Select(m => m.ToBllPerson());
        }
        #endregion
    }
}
