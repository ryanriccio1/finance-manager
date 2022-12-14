//  Program: A simple program to manage financial investments.
//  Makes API calls to get current stock data. Stores data in local SQLite DB.
//  Specifications: performs what if calculations, and allows for storing of user's stocks
//  for future reference to calculate total portfolio value
//  
//  Authors: Ryan Riccio, Young Gi Hong
//  Date: December 14th, 2022
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services;
using FinanceManager.WPF.ViewModels;
using FinanceManager.FinancialModelAPI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FinanceManager.Domain.Services.TransactionServices;
using FinanceManager.EntityFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using FinanceManager.EntityFramework;
using FinanceManager.WPF.State.Navigators;
using FinanceManager.WPF.ViewModels.Factories;
using FinanceManager.Domain.Services.AuthenticationServices;
using Microsoft.AspNet.Identity;
using FinanceManager.WPF.State.Authenticators;
using FinanceManager.WPF.State.Accounts;
using FinanceManager.WPF.State.Assets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FinanceManager.FinancialModelAPI;
using FinanceManager.WPF.HostBuilders;

namespace FinanceManager.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {   // on initialization, build the host
            _host = CreateHostBuilder().Build();
        }

        // build the app from the host builder / dependecy injection
        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddFinanceAPI()
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            // initiate the connection to the db
            FinanceManagerDbContextFactory contextFactory = _host.Services.GetRequiredService<FinanceManagerDbContextFactory>();
            using(FinanceManagerDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            // instatiate the main window
            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {   // dispose of the host when we exit
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
