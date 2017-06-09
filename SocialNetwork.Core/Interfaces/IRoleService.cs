using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces
{
    public interface IRoleService: IService<Role>
    {
        Role FindByName(string name);
        Role GetRole(int id);
    }
}
