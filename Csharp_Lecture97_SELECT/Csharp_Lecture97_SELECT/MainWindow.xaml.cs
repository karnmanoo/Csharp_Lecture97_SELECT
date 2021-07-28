using System;
using System.Collections.Generic;
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

namespace Csharp_Lecture97_SELECT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccess.InitializeDatabase();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.AddData(uidTxt.Text, firstNameTxt.Text, lastNameTxt.Text, emailTxt.Text);
            MessageBox.Show("Add customer data" + "\n" +
                "Customer ID : " + uidTxt.Text + "\n" +
                "First Name : " + firstNameTxt.Text + "\n" +
                "Last Name : " + lastNameTxt.Text + "\n" +
                "Email Address : " + emailTxt.Text,"Add cutomer data");
            uidTxt.Clear();
            firstNameTxt.Clear();
            lastNameTxt.Clear();
            emailTxt.Clear();
        }

        private void showBtn_Click(object sender, RoutedEventArgs e)
        {
            string customerShow = "";
            foreach(string data in DataAccess.GetData())
            {
                customerShow = customerShow + data + "\n";
            }
            MessageBox.Show("Customer Data as below:" + "\n" + customerShow, "Show all customer data");
        } 

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.ClearData();
            uidTxt.Clear();
            firstNameTxt.Clear();
            lastNameTxt.Clear();
            emailTxt.Clear();
            MessageBox.Show("Customer Data has been cleared","Clear customer data");
        }
    }
}
