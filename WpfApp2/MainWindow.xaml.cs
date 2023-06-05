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
using WpfApp2.View;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Module(object sender, RoutedEventArgs e)
        {
            ViewModule but = new ViewModule();
            but.Show();
            this.Close();
        }
        private void Button_BD(object sender, RoutedEventArgs e)
        {
            DBMain but = new DBMain();
            but.Show();
            this.Close();
        }
    }
}
