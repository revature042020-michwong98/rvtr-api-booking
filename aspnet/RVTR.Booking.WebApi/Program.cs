using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RVTR.Booking.DataContext;

namespace RVTR.Booking.WebApi
{
  /// <summary>
  ///
  /// </summary>
  public class Program
  {
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      await CreateDbContextAsync(host);
      await host.RunAsync();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      });

    /// <summary>
    ///
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static async Task CreateDbContextAsync(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var provider = scope.ServiceProvider;
        var context = provider.GetRequiredService<BookingContext>();

        await context.Database.EnsureCreatedAsync();
      }
    }
  }
}
