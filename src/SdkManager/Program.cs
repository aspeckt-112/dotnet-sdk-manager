using Avalonia;
using System;

namespace SdkManager
{
    internal sealed class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // TODO Look at global error handler: https://docs.avaloniaui.net/docs/concepts/unhandledexceptions
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
        }
    }
}