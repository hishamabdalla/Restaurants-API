using Restaurants.Application.URl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Common.Pagination
{
    public class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePagedReponse<T>(IEnumerable<T> pagedData, int pageNumber,int pageSize, int totalRecords,IUriService uriService,string route)
        {

            var respose = new PagedResponse<IEnumerable<T>>(pagedData, pageNumber, pageSize, totalRecords);
            var totalPages = ((double)totalRecords / (double)pageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            respose.NextPage =
                pageNumber >= 1 && pageNumber < roundedTotalPages
                    ? uriService.GetPageUri(new PaginationQuery<object>(pageNumber + 1, pageSize), route)
                    : null!;

            respose.PreviousPage =
                pageNumber - 1 >= 1 && pageNumber <= roundedTotalPages
                    ? uriService.GetPageUri(new PaginationQuery<object>(pageNumber - 1, pageSize), route)
                    : null!;

            respose.FirstPage = uriService.GetPageUri(new PaginationQuery<object>(1, pageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationQuery<object>(roundedTotalPages, pageSize), route);
            return respose;

            
        }
    }
}
