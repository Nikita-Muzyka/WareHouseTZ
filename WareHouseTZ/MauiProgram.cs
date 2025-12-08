using Microsoft.Extensions.Logging;
using WareHouseTZ.Date;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;

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
            builder.Services.AddScoped<IDBService,DBService>();
            builder.Services.AddScoped<IDisplayService,DisplayService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
