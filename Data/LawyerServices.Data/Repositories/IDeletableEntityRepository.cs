using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Repositories
{
    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
         where TEntity : class, IDeletable
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
