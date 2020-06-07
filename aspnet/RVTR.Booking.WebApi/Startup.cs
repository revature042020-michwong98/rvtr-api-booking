using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using zipkin4net.Middleware;

namespace RVTR.Booking.WebApi
{
  /// <summary>
  ///
  /// </summary>
  public class Startup
  {
    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public IConfiguration Configuration { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddApiVersioning(options =>
      {
        options.ReportApiVersions = true;
      });

      services.AddControllers();
      services.AddCors(options =>
      {
        options.AddPolicy("Public", policy =>
        {
          policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
      });

      services.AddDbContext<BookingContext>(options =>
      {
        options.UseNpgsql(Configuration.GetConnectionString("pgsql"), options =>
        {
          options.EnableRetryOnFailure(3);
        });
      });

      services.AddScoped<ClientZipkinMiddleware>();
      services.AddScoped<UnitOfWork>();
      services.AddSwaggerGen();
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ClientSwaggerOptions>();
      services.AddVersionedApiExplorer(options =>
      {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
      });
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <param name="descriptionProvider"></param>
    /// <param name="hostEnvironment"></param>
    public void Configure(IApplicationBuilder applicationBuilder, IApiVersionDescriptionProvider descriptionProvider, IWebHostEnvironment hostEnvironment)
    {
      if (hostEnvironment.IsDevelopment())
      {
        applicationBuilder.UseDeveloperExceptionPage();
      }

      applicationBuilder.UseZipkin();
      applicationBuilder.UseTracing("bookingapi.rest");
      applicationBuilder.UseHttpsRedirection();
      applicationBuilder.UseRouting();
      applicationBuilder.UseSwagger();
      applicationBuilder.UseSwaggerUI(options =>
      {
        foreach (var description in descriptionProvider.ApiVersionDescriptions)
        {
          options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
        }
      });

      applicationBuilder.UseCors();
      applicationBuilder.UseAuthorization();
      applicationBuilder.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
