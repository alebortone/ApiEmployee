using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Application.Interfaces;
using MinhaApi.Infrastructure.Data;





public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
    {
        protected readonly ConnectionContext context;

        public RepositoryGeneric(ConnectionContext context)
        {
            this.context = context;
        }


        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();

        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
        public async Task<T?> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity, int id)
        {
            var entitie = await context.Set<T>().FindAsync(id);
            if (entitie == null)
                throw new Exception("Entidade não encontrada ou não existente");

            // context.Entry(entitie).State = EntityState.Detached;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

    }
   

