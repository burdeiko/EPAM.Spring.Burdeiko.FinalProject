using System;

namespace SocialNetwork.Dal.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
    }
}
