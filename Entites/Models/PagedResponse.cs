using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCunt { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }

        public PagedResponse(List<T> data, int totalCount,int pageNumber ,int pageSize)
        {
            Data = data;
            TotalCunt = totalCount; 
            PageNumber = pageNumber;
            PageSize = pageSize;

            TotalPages= (int)Math.Ceiling(totalCount/(double)pageSize);
        }
    }
}
