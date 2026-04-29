

namespace MinhaAPI.Aplication.Abstracoes.Query
{
    public record PagedQuery
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; }
        public string? Filter { get; set; }
    }
}
