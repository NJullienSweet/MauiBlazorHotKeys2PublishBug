using Microsoft.Extensions.Logging;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace MauiBlazorHotKeys2PublishBug
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
				});

			builder.Services.AddMauiBlazorWebView();
			builder.Services.AddHotKeys2();

#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
