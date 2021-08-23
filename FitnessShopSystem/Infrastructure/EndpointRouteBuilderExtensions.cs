namespace FitnessShopSystem.Infrastructure
{
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Builder;

    public static class EndpointRouteBuilderExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}");
    }
}
