using Sample_App.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sample_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {
            InitializeComponent();
            int[] source = new int[] { 1, 2, 3, 4, 5 };
            lstVwData.ItemsSource = source;
        }

        public void LoadUser(string name)
        {
            UserNameLbl.Content = name;
            DbHandler dbHandler = new DbHandler();
            dbHandler.InsertUserInformation(name);
            LoadLastLoginDetails(dbHandler);
        }

        private void LoadLastLoginDetails(DbHandler dbHandler)
        {
            List<string> source = new List<string>();
            try
            {
                foreach (DataRow row in dbHandler.ReadUserInformation().Tables[0].Rows)
                {
                    source.Add(string.Format("{0}: {1} - {2}", row["Id"], row["Name"], row["LoginDate"]));
                }
            }
            catch(Exception ex)
            {
                Logger.WriteLog("Error: " + ex.Message);
            }

            lstVwData.ItemsSource = source;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
