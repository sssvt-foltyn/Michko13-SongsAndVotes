using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SongsAndVotes.Server
{



    public static class ProductsLinkEndpointRouteBuilderExtensions
    {



        private const string DefaultDisplayName = "Products Link";



        public static IEndpointConventionBuilder MapProductsLink(this IEndpointRouteBuilder endpoints, string pattern)
        {
            if (endpoints == null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            return MapProductsLinkCore(endpoints, pattern);
        }



        private static IEndpointConventionBuilder MapProductsLinkCore(IEndpointRouteBuilder endpoints, string pattern)
        {
            var pipeline = endpoints.CreateApplicationBuilder()
                .UseMiddleware<ProductsLinkMiddleware>()
                .Build();

            return endpoints.Map(pattern, pipeline).WithDisplayName(DefaultDisplayName);
        }



    }



}
