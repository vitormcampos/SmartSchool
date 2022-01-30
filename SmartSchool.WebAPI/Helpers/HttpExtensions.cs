using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.WebAPI.Helpers
{
    public static class HttpExtensions
    {
        public static void AddPagination(this HttpResponse response,
            int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var header = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var headerFormatter = new JsonSerializerSettings();
            headerFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(header, headerFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}