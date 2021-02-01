using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SwashbuckleReDocDemo.Api.Extensions;

namespace SwashbuckleReDocDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            
            services.AddErrorHandling();
            
            services.AddControllers(options =>
                {
                    options.ReturnHttpNotAcceptable = true;
                    options.RespectBrowserAcceptHeader = true;
                    
                    options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                    options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
                    options.Filters.Add(new ProducesResponseTypeAttribute(
                        typeof(ProblemDetails),
                        StatusCodes.Status500InternalServerError)
                    );
                    options.Filters.Add(new ProducesResponseTypeAttribute(
                        StatusCodes.Status406NotAcceptable)
                    );
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            
            services.AddSwaggerGen(c =>
            {
                string apiDescriptionFilePath = Path.Combine(AppContext.BaseDirectory, "OpenApiDescription.md");
                string apiDescription = File.ReadAllText(apiDescriptionFilePath);
                
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = apiDescription,
                    Title = "Swashbuckle & ReDoc Demo", 
                    Version = "v1",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Hannes Karask",
                        Url = new Uri("https://karask.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Sample license",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                
                c.AddServer(new OpenApiServer
                {
                    Description = "Local",
                    Url = "http://localhost:5000"
                });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \
                      \
                      Generate a token using the [OAuth](#tag/OAuth) endpoints and include it in the Authorization header as follows: \
                      `Authorization: Bearer <token>`",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwashbuckleReDocDemo.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                endpoints.MapGet("/docs", async context =>
                {
                    context.Response.ContentType = "text/html; charset=UTF-8";
                    string filePath = Path.Combine(AppContext.BaseDirectory, "ReDoc.html");
                    await context.Response.SendFileAsync(filePath);
                });
            });
        }
    }
}