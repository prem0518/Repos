using System.Collections.Generic;

namespace TimsProject.Infrastructure
{
    public interface ICRUDRepository<TEntity, TIdentity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetDetails(TIdentity id);
        void Create(TEntity item);
        
        void Update(TEntity item);
        void Delete(TIdentity id);
    }
}