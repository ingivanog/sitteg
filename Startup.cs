﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GuanajuatoAdminUsuarios.Services;

using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Utils;

namespace GuanajuatoAdminUsuarios
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
            // Configure cookie based authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        // Specify where to redirect un-authenticated users
                        options.LoginPath = "/login";

                        // Specify the name of the auth cookie.
                        // ASP.NET picks a dumb name by default.
                        options.Cookie.Name = "gto_admin_auth_cookie";
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                        options.SlidingExpiration = true;
                    });

            services.AddRouting(setupAction =>
            {
                setupAction.LowercaseUrls = true;
            });
            // Add framework services.
            //Added for session state
            services.AddDistributedMemoryCache();

            //configuracion de session
            services.AddSession();

            //Hacer accesible la cadena de conexion a la base desde la clase
            // services.AddTransient<ConexionBD>();

            //Servicios
            services.AddScoped(typeof(IPdfGenerator<>), typeof(PdfGenerator<>));
            services.AddScoped<ISqlClientConnectionBD, SqlClientConnectionBD>();
            services.AddScoped<IMarcasVehiculos, MarcasVehiculoService>();
            services.AddScoped<ISubmarcasVehiculos, SubmarcasVehiculosService>();
            services.AddScoped<IDependencias, DependenciasService>();
            services.AddScoped<IOficiales, OficialesService>();
            services.AddScoped<IPlacaServices, PlacaServices>();
            services.AddScoped<ILiberacionVehiculoService, LiberacionVehiculoService>();
            services.AddScoped<ITransitoTransporteService, TransitoTransporteService>();
            services.AddScoped<IGruasService, GruasService>();
            services.AddScoped<IPadronDepositosGruasService, PadronDepositosGruasService>();
            services.AddScoped<IMunicipiosService, MunicipiosService>();
            services.AddScoped<IConcesionariosService, ConcesionariosService>();
            services.AddScoped<IReporteAsignacionService, ReporteAsignacionService>();
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IGruas2Service, Gruas2Service>();
            services.AddScoped<IPensionesService, PensionesService>();
            services.AddScoped<IDelegacionesService, DelegacionesService>();
            


            services
               .AddControllersWithViews()
               .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddKendo();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Inicio/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // implementa la validacion de sesion
            app.UseSession();
            app.UseRouting();


            //Autorizacion y autenticacion de usuario
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Inicio}/{action=Inicio}/{id?}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "defaultSitteg",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Inicio}/{action=Inicio}/{id?}");
            });

        }
    }
}
