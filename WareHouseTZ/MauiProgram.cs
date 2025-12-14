using Microsoft.Extensions.Logging;
using WareHouseTZ.Date;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.View;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddScoped<DBApplication>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModal>();
            builder.Services.AddTransient<CreateProductView>();
            builder.Services.AddTransient<CreateProductViewModal>();
            builder.Services.AddTransient<EditProductView>();
            builder.Services.AddTransient<EditProductViewModal>();
            builder.Services.AddTransient<ComingView>();
            builder.Services.AddTransient<ComingViewModel>();
            builder.Services.AddTransient<CreateComingView>();
            builder.Services.AddTransient<CreateComingViewModal>();
            builder.Services.AddScoped<IDBService,DBService>();
            builder.Services.AddScoped<IDisplayService,DisplayService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
