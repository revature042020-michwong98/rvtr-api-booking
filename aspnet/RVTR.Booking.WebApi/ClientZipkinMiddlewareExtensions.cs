using Microsoft.AspNetCore.Builder;

namespace RVTR.Booking.WebApi
{
  internal static class ZipkinClientMiddlewareExtensions
  {
    public static IApplicationBuilder UseZipkin(this IApplicationBuilder applicationBuilder)
    {
      return applicationBuilder.UseMiddleware<ClientZipkinMiddleware>();
    }
  }
}
