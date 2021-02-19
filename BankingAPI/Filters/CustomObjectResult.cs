using BankingAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.ModelBinding;

namespace BankingAPI.Filters
{
    public class CustomObjectResult : ObjectResult
    {
        private static object CreateResponse(string response)
        {
            return new { response=response};
        }

        public CustomObjectResult(HttpStatusCode statusCode, string response) : base(CreateResponse(response))
        {
            StatusCode = (int)statusCode;
        }
    }
}

