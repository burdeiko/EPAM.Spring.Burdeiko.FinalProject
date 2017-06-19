using System.Data.Entity;
using SocialNetwork.Dal.Interfaces;
using System;
using NLog;
using LoggerImplementation;

namespace SocialNetwork.Dal.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ILogger logger = new LoggerFactory().GetLogger();
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            if (Context != null)
            {
                try
                {
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Info("Unhandled exception");
                    logger.Error(e.StackTrace);
                    throw;
                }
            }
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
