using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sample_App
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
            UserNameTxt.Text = "admin";
            PasswordTxt.Password = "admin";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isLoginValid = false;
            if(string.IsNullOrEmpty(UserNameTxt.Text) || string.IsNullOrEmpty(PasswordTxt.Password))
            {
                MessageBox.Show("Please enter the Login Name/Password.");
            }
            else
            {
                if (UserNameTxt.Text.ToLower().Equals("admin") && PasswordTxt.Password.ToLower().Equals("admin"))
                    isLoginValid = true;
                else
                    MessageBox.Show("Incorrect Login Name/Passowrd!");
            }

            if(isLoginValid)
            {
                MainWindow window = new MainWindow();
                window.LoadUser(UserNameTxt.Text);
                window.Show();
                this.Hide();
            }
        }
    }
}
