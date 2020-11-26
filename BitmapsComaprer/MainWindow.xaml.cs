using System.Windows;

namespace BitmapsComaprer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWidowViewModel mainWidowViewModel)
        {
            InitializeComponent();
            DataContext = mainWidowViewModel;     
        }
    }
}
