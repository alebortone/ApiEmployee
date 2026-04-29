using Microsoft.EntityFrameworkCore;
using MinhaApi.Infrastructure.Data;
using MinhaAPI.Aplication.Abstracoes.Filter;
using MinhaAPI.Aplication.Interfaces;
using MinhaAPI.Aplication.DTOs;

public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
    {
        protected readonly ConnectionContext context;
        protected virtual List<string> OrderColumns { get; } = new();
        protected virtual List<FilterColumn> FilterColumns { get; } = new();
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

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
        public async Task<T?> GetById(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }


        public async Task<PagedResponse<T>> GetPagined(
            int page,
            int limit,
            List<FilterObject>? filters,
            string? orderBy,
            string? orderDirection
        )
        {
            var query = context.Set<T>().AsQueryable();

            //  FILTRO (limitado aos permitidos)
             if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    var allowed = FilterColumns
                        .FirstOrDefault(f => f.Column.ToLower() == filter.SearchColumn.ToLower());

                    if (allowed != null)
                    {
                        // versão simples (string apenas)
                        if (allowed.Type == "string")
                        {
                            query = query.Where(e => EF.Property<string>(e, allowed.Column)
                                .Contains(filter.SearchValue)
                        );
                    }
                }
            }
        }

            //  TOTAL
            var total = await query.CountAsync();

            //  ORDENAÇÃO
            if (!string.IsNullOrEmpty(orderBy) &&
                OrderColumns.Contains(orderBy))
            {
                query = orderDirection == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, orderBy))
                    : query.OrderBy(e => EF.Property<object>(e, orderBy));
            }
            else
            {
                query = query.OrderBy(e => EF.Property<object>(e, "Id"));
            }

            // PAGINAÇÃO
                var data = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return new PagedResponse<T>
            {
                Total = total,
                Data = data
            };
    }

}
   

