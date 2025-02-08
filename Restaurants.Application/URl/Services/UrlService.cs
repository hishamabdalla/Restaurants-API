using Microsoft.AspNetCore.WebUtilities;
using Restaurants.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.URl.Services
{
    public class UrlService<TData>:IUriService
    { 
        private readonly string _baseUri;

        public UrlService(string baseUri)
        {
            _baseUri = baseUri;
        }

       
       public Uri GetPageUri(PaginationQuery<object> filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
