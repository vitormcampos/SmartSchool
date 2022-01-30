using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.WebAPI.Helpers
{
    public class PaginationList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public PaginationList(List<T> items, int pageNumber, int pageSize, int totalCount)
        {
            this.CurrentPage = pageNumber;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(Count / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PaginationList<T>> CreateAsync(
            IQueryable<T> src, int pageNumber, int pageSize) 
        {
            var count = await src.CountAsync();
            var items = await src.Skip(pageNumber -1 * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginationList<T>(items, count, pageNumber, pageSize);
        }
        
    }
}