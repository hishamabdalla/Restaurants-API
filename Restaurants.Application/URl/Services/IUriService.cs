using Restaurants.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.URl.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationQuery<object> filter, string route);
    }
}
