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

namespace seting_sqlite
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
            //DataAccess.AddData("Miss. Mali Jaidee");
        }

        private void showAllBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> responseData = DataAccess.GetData();
            string response = string.Empty;
            foreach (string text in responseData)
            {
                response += text + "\n";
            }
            MessageBox.Show(response);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.AddData(inputTxt.Text);
            inputTxt.Text = string.Empty;
        }

    }
}
