﻿using C1.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace C1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var navigationService = ServiceProvider.GetRequiredService<MainWindow>();
            navigationService.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<NavigationService>();
            services.AddTransient<MainWindow>();
            services.AddScoped<PE_PRN_Fall22B1Context>();
            //services.AddTransient<MemberManagement>();
            //services.AddTransient<OrderManagement>();
            //services.AddTransient<ProfileManagement>();
            //services.AddTransient<ProductManagement>();
            //services.AddScoped<IMemberRepository, MemberRepository>();
            //services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
