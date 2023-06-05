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
using System.Windows.Shapes;

namespace WpfApp2.View
{
    /// <summary>
    /// Логика взаимодействия для DBMain.xaml
    /// </summary>
    public partial class DBMain : Window
    {
        public DBMain()
        {
            InitializeComponent();
        }

        private void Button_CalcCoord(object sender, RoutedEventArgs e)
        {
            CalculationCoordinates but = new CalculationCoordinates();
            but.Show();
            this.Close();
        }
        private void Button_RefPoint(object sender, RoutedEventArgs e)
        {
            ReferencePoint but = new ReferencePoint();
            but.Show();
            this.Close();
        }
        private void Button_SampPointCoord(object sender, RoutedEventArgs e)
        {
            SamplePointCoordinates but = new SamplePointCoordinates();
            but.Show();
            this.Close();
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow but = new MainWindow();
            but.Show();
            this.Close();
        }
    }
}
