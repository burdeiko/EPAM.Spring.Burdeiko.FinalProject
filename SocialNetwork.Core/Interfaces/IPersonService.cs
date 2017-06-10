using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces
{
    public interface IPersonService: IService<Person>
    {
        Person FindByFirstName(string firstName);
        Person GetById(int id);
    }
}
