using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            builder.Services.AddScoped<IDBService, DBService>();
            builder.Services.AddScoped<IDisplayService, DisplayService>();

            var connection = "Server=MYZUKA\\SQLEXPRESS;Database=WareHouseTZ;Trusted_Connection=true;TrustServerCertificate=true;";
            builder.Services.AddDbContext<DBApplication>(option => option.UseSqlServer(connection));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
