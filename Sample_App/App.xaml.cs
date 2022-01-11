using Sample_App.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sample_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            TestDB();
            base.OnStartup(e);
        }

        private void TestDB()
        {
            DbHandler dbHandler = new DbHandler();
            bool isconnected = dbHandler.TestConnection();
            if (!isconnected)
            {
                MessageBox.Show("Database not found. So closing the app.");
                System.Windows.Application.Current.Shutdown();
            }            
        }
    }
}
