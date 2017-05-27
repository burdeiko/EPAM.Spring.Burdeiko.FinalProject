using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces
{
    public interface IService<T>
    {
        void CreateEntity(T entity);
        IEnumerable<T> GetAllEntities();
        void DeleteEntity(T entity);
    }
}
