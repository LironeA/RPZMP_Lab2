﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WarehouseApp.Services;
using WarehouseApp.ViewModels;
using WarehouseApp.Views;

namespace WarehouseApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                      .UseMauiApp<App>()
                      .UseMauiCommunityToolkit()
                      .ConfigureFonts(fonts =>
                      {
                          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                      });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<ProductsPage>();
            builder.Services.AddTransient<ProductsVM>();
            builder.Services.AddTransient<IProductService, ProductService>();

            return builder.Build();
        }
    }
}
