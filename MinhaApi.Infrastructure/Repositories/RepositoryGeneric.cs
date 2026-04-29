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

            query = ApplyFilters(query, filters);


            query = ApplyOrdering(query, orderBy, orderDirection);

        //  TOTAL
        var total = await query.CountAsync();

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
        protected IQueryable<T> ApplyFilters(IQueryable<T> query, List<FilterObject>? filters)
        {
            if (filters == null || !filters.Any())
                return query;

            foreach (var filter in filters)
            {
                var allowed = FilterColumns
                    .FirstOrDefault(f => f.Column.ToLower() == filter.SearchColumn.ToLower());
                                                                       
                if (allowed != null && allowed.Type == "string")
                {
                    // versão simples (string apenas)
                    query = query.Where(e => EF.Property<string>(e, allowed.Column)
                        .Contains(filter.SearchValue));
                }
            }

            return query;
        }

        protected IQueryable<T> ApplyOrdering(IQueryable<T> query, string? orderBy, string? orderDirection)
        {
            if (!string.IsNullOrEmpty(orderBy) && OrderColumns.Contains(orderBy))
            {
                return orderDirection == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, orderBy))
                    : query.OrderBy(e => EF.Property<object>(e, orderBy));
            }

            // Ordenação padrão caso nenhuma seja passada
            return query.OrderBy(e => EF.Property<object>(e, "Id"));
        }
   
}
   

