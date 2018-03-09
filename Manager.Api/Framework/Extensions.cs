using Microsoft.AspNetCore.Http;

namespace Manager.Api.Framework
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage,
            int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, 
                totalItems, totalPages);

            response.Headers.Add("Pagination",
               Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
            response.Headers.Add("access-control-expose-headers", "Pagination");
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }
    }
}