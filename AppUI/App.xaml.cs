using E_CommerceApp;
using System.Windows;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IDataOperations myData = GlobalConfig.Inject();

            IPagination paginator = GlobalConfig.InjectPaginator();

            MainWindow mw = new MainWindow(myData, paginator);
            mw.Show();
        }
    }
}