using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuvio.Kernel.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParentCare.Model.Medications.Commands;
using ParentCare.Model.Users;
using ParentCare.Plugins.Database;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Kuvio.Kernel.Database.SqlServer;

namespace ParentCare.API
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
            AddRepositories(services);
            AddQueries(services);

            services.AddControllers();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"])),
                    ClockSkew = TimeSpan.Zero
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddRepositories(IServiceCollection services)
        {
            // TODO: Verificar se deixamos transient ou scoped
            services.AddDbContext<ParentCareContext>(
                options => options.UseSqlServer(Configuration["ConnectionStrings:Database"]), ServiceLifetime.Transient);

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ParentCareContext>()
                .AddDefaultTokenProviders();

            services.AddTransient(typeof(IRepository<>), typeof(DbRepository<>));

            //services.AddDbContext<StockMeContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Database"]));

            //services.AddDbContext<StockMeContext>(options => options.UseInMemoryDatabase("MemoryDb"));
            //services.AddTransient(typeof(IRepository<>), typeof(InMemoryDbRepository<>));

            //services.AddScoped(options => new StorageContext(Configuration["ConnectionStrings:Storage"]));
            //services.AddTransient<IMediaRepository, MediaRepository>();
        }

        private void AddQueries(IServiceCollection services)
        {
            // Contatos
            //services.AddTransient<CarregarContatosQuery>();
        }

        private void AddCommands(IServiceCollection services)
        {
            // Medication
            services.AddTransient<CreateMedicationAlertCommand>();
            services.AddTransient<TakeMedicationCommand>();
        }
    }
}
