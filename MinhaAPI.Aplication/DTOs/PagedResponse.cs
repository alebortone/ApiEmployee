using System;
using System.Collections.Generic;
using System.Text;

namespace MinhaAPI.Aplication.DTOs
{
    public class PagedResponse<T>
    {
        public int Total { get; set; }
        public List<T> Data { get; set; } = new();
    }


}
