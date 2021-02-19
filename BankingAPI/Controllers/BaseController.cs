using BankingAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BankingAPI.Controllers
{
    public class BaseController : Controller
    {


        [System.Web.Http.NonAction]
        public IActionResult CustomErrorResult(ModelStateDictionary modelState)
        {
            var errorMessages = new List<string>();

            foreach (var state in ModelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    errorMessages.Add(error.ErrorMessage?.Replace("\r\n", " | "));
                }
            }

            var errorMessage = errorMessages.Any() ? errorMessages.First() : "";

            return CustomResult(HttpStatusCode.BadRequest, (string.IsNullOrWhiteSpace(errorMessage) || errorMessage.Contains("token is not a valid") || errorMessage.Contains("converting value")) ? "Please check the input(s) supplied" : errorMessage);
        }

        [System.Web.Http.NonAction]
        public ObjectResult CustomResult(HttpStatusCode statusCode, string responseDescription)
        {


            return new CustomObjectResult(statusCode, responseDescription?.Replace("\r\n", " | "));
        }

     
        [System.Web.Http.NonAction]

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
        [System.Web.Http.NonAction]
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }  
 }


