using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SongsAndVotes.Server
{



    public class ProductsLinkMiddleware
    {



        private readonly LinkGenerator _linkGenerator;



        public ProductsLinkMiddleware(RequestDelegate next, LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }



        public async Task InvokeAsync(HttpContext httpContext)
        {
            string action = httpContext.Request.Query["a"];
            string controller = httpContext.Request.Query["c"];

            //var url = _linkGenerator.GetPathByAction("ListProducts", "Store");
            //var url = _linkGenerator.GetPathByAction("Get", "WeatherForecast");
            var url = _linkGenerator.GetPathByAction(action, controller);
            string linkGeneratorType = _linkGenerator?.GetType().FullName ?? "(null)";
            string urlType = url?.GetType().FullName ?? "(null)";

            httpContext.Response.ContentType = "text/plain";

            await httpContext.Response.WriteAsync($"Link generator: {linkGeneratorType}    Url: {urlType}    ");
            await httpContext.Response.WriteAsync($"Go to {url} to see our products.   ");
        }



    }



}
