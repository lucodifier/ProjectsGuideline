using Guideline.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//public class AuthorizeAttribute : Attribute, IAuthorizationFilter
//{
//    public void OnAuthorization(AuthorizationFilterContext context)
//    {
//        if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any()) return;

//        var user = context.HttpContext.User;
//        //var user = (UserViewModel)context.HttpContext.Items["User"];
//        //if (user == null)
//        //{
//        //    // not logged in
//        //    context.Result = new JsonResult(new { message = "Não autorizado. Validar o Token." }) { StatusCode = StatusCodes.Status401Unauthorized };
//        //}
//    }
//}