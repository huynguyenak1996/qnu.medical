using Medical.Data;
using Medical.Entities.System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Medical.API.Major
{
    public static class ExtServiceCollection
    {
        static readonly string _migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // options for user and password can be set here
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            services.AddTransient<AppDbContext>();
           // services.AddTransient<MajorDbContext>();
            //services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
            //services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
        

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContextPool<AppDbContext>(options =>
            {
                string useSqLite = Startup.Configuration["Data:useSqLite"];
                string useInMemory = Startup.Configuration["Data:useInMemory"];
                if (useInMemory.ToLower() == "true")
                {
                    options.UseInMemoryDatabase("Medical.API"); // Takes database name
                }
                else if (useSqLite.ToLower() == "true")
                {
                    var connection = Startup.Configuration["Data:SqlLiteConnectionString"];
                    options.UseSqlite(connection);
                    options.UseSqlite(connection, b => b.MigrationsAssembly(_migrationsAssembly));

                }
                else
                {

                    var connection = Startup.Configuration["Data:SqlServerConnectionString"];
                    options.UseSqlServer(connection);
                    options.UseSqlServer(connection, b => b.MigrationsAssembly(_migrationsAssembly));
                }
            });

            //services.AddDbContext<MajorDbContext>(options =>
            //{
            //    string useSqLite = Startup.Configuration["Data:useSqLite"];
            //    string useInMemory = Startup.Configuration["Data:useInMemory"];
            //    if (useInMemory.ToLower() == "true")
            //    {
            //        options.UseInMemoryDatabase("Museum.WebData"); // Takes database name
            //    }
            //    else if (useSqLite.ToLower() == "true")
            //    {
            //        var connection = Startup.Configuration["Data:SqlLiteConnectionString"];
            //        options.UseSqlite(connection);
            //        options.UseSqlite(connection, b => b.MigrationsAssembly(_migrationsAssembly));

            //    }
            //    else
            //    {
            //        var connection = Startup.Configuration["Data:SqlServerConnectionString"];
            //        options.UseSqlServer(connection);
            //        options.UseSqlServer(connection, b => b.MigrationsAssembly(_migrationsAssembly));
            //    }
            //});
            return services;
        }

        /*
        public static IServiceCollection AddCustomIdentityServer(this IServiceCollection services, IHostingEnvironment env)
        {
            string useSqLite = Startup.Configuration["Data:useSqLite"];
            string useInMemory = Startup.Configuration["Data:useInMemory"];
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
              .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    if (useSqLite.ToLower() == "true")
                    {
                        var connection = Startup.Configuration["Data:SqlLiteConnectionString"];
                        options.ConfigureDbContext = b =>
                        b.UseSqlite(connection,
                            sql => sql.MigrationsAssembly(_migrationsAssembly));

                    }
                    else
                    {
                        var connection = Startup.Configuration["Data:SqlServerConnectionString"];
                        options.ConfigureDbContext = b =>
                        b.UseSqlServer(connection,
                            sql => sql.MigrationsAssembly(_migrationsAssembly));
                    }

                })
                 .AddOperationalStore(options =>
                 {
                     if (useSqLite.ToLower() == "true")
                     {
                         var connection = Startup.Configuration["Data:SqlLiteConnectionString"];
                         options.ConfigureDbContext = b =>
                         b.UseSqlite(connection,
                             sql => sql.MigrationsAssembly(_migrationsAssembly));

                     }
                     else
                     {
                         var connection = Startup.Configuration["Data:SqlServerConnectionString"];
                         options.ConfigureDbContext = b =>
                         b.UseSqlServer(connection,
                             sql => sql.MigrationsAssembly(_migrationsAssembly));
                     }

                     // this enables automatic token cleanup. this is optional.
                     options.EnableTokenCleanup = false;
                     // options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
                 });
            ;
            if (env.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                builder.AddDeveloperSigningCredential();
                //throw new Exception("need to configure key material");
            }
            return services;
        }
        */
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
              // https://console.developers.google.com/projectselector/apis/library?pli=1
              .AddGoogle(options =>
              {
                  options.ClientId = Startup.Configuration["Authentication:Google:ClientId"];
                  options.ClientSecret = Startup.Configuration["Authentication:Google:ClientSecret"];
              })
              // https://developers.facebook.com/apps
              .AddFacebook(options =>
              {
                  options.AppId = Startup.Configuration["Authentication:Facebook:AppId"];
                  options.AppSecret = Startup.Configuration["Authentication:Facebook:AppSecret"];
              })
              ////https://apps.twitter.com/
              .AddTwitter(options =>
              {
                  options.ConsumerKey = Startup.Configuration["Authentication:Twitter:ConsumerKey"];
                  options.ConsumerSecret = Startup.Configuration["Authentication:Twitter:ConsumerSecret"];
              })
              ////https://apps.dev.microsoft.com/?mkt=en-us#/appList
              .AddMicrosoftAccount(options =>
              {
                  options.ClientId = Startup.Configuration["Authentication:Microsoft:ClientId"];
                  options.ClientSecret = Startup.Configuration["Authentication:Microsoft:ClientSecret"];
              });

            return services;
        }
    }
}
