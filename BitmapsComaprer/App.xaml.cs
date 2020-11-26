using Autofac;
using System.Windows;

namespace BitmapsComaprer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<MainWindow>();
            containerBuilder.RegisterType<MainWidowViewModel>();

            var container = containerBuilder.Build();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }       
    }
}
