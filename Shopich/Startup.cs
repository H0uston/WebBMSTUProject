using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopich.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Shopich.Config;
using Shopich.Repositories.interfaces;
using Shopich.Repositories.implementations;
using System;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Shopich
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
            services.AddDbContext<ShopichContext>(option =>
                option.UseNpgsql(Configuration.GetConnectionString("ShopichDB")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = jwtOptions.AUDIENCE,
                        ValidateLifetime = true,

                        IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            scopeInterfaceRepository(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Shopich API",
                    Description = "Official shopich ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Official telegram channel",
                        Email = string.Empty,
                        Url = new Uri("https://t.me/mp4_thread"),
                    }
                });

                // Bearer token authentication
                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("Bearer", securityDefinition);

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };
                //c.AddSecurityRequirement(securityRequirements);
                c.OperationFilter<AuthResponsesOperationFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void scopeInterfaceRepository(IServiceCollection services)
        {
            services.AddScoped<ICategory, PostgreSQLCategoryRepository>();
            services.AddScoped<ICategories, PostgreSQLCategoriesRepository>();
            services.AddScoped<IOrder, PostgreSQLOrderRepository>();
            services.AddScoped<IOrders, PostgreSQLOrdersRepository>();
            services.AddScoped<IProduct, PostgreSQLProductRepository>();
            services.AddScoped<IReview, PostgreSQLReviewRepository>();
            services.AddScoped<IRole, PostgreSQLRoleRepository>();
            services.AddScoped<IUser, PostgreSQLUserRepository>();
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ShopichContext>()) // TODO
                {
                    context.Database.Migrate();
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api/v1";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/admin/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
            {
                var securityRequirement = new OpenApiSecurityRequirement()
            {
                {
                    // Put here you own security scheme, this one is an example
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            };
                operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            }
        }
    }
}
