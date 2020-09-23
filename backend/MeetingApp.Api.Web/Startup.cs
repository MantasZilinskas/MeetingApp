using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MeetingApp.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using MeetingApp.Api.Data.Repository.Interfaces;
using MeetingApp.Api.Data.Repository.Implementation;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Business.Services.Implementation;
using AutoMapper;
using System.Reflection;
using System;

namespace MeetingApp.Api.Web
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
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen();
            services.AddDbContext<MeetingAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("default")));

            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<ITodoItemService, TodoItemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "Meeting app API V1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
