using Ludo.Boards;
using Ludo.GameModes;
using Ludo.Models;
using Ludo.UserInterfaces;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using LudoLibrary.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LudoWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //TODO:: REMOVE
            Game.CreateInstance();
            
            Game.Instance.Board = new WebBoard();
            Game.Instance.GameMode = new WebMode();
            Game.Instance.UserInterface = new WebUi();
            
            Game.Instance.Reset();

            Game.Instance.NewPlayer("Player 1", 'A');
            Game.Instance.NewPlayer("Player 2", 'B');
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDistributedMemoryCache();

            services.AddDbContext<LudoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LudoContextConnection"))
            );
            
            services.AddTransient<IService<Room>, RoomService>();
            services.AddTransient<IService<User>, UserService>();
            services.AddTransient<IService<Command>, CommandService>();
            
            services.AddTransient<IScoreService, ScoreService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IRatingService, RatingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}