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
using System.Data.SqlClient;
using System.Data;

namespace WpfApp2.View
{
    /// <summary>
    /// Логика взаимодействия для ReferencePoint.xaml
    /// </summary>
    public partial class ReferencePoint : Window
    {
        private const string ConnectionString = @"Data Source=DESKTOP-R71RRJI\EVA;Initial Catalog=ArealSourceDB;Integrated Security=True;TrustServerCertificate=True";


        private readonly DataTable referencePointTable = new DataTable();

        public ReferencePoint()
        {
            InitializeComponent();

            LoadReferencePoints();
        }

        private void LoadReferencePoints()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var adapter = new SqlDataAdapter("SELECT ID_RefPoint, Latitude, Longitude, Concentration FROM ReferencePoint", connection);

                adapter.Fill(referencePointTable);

                referencePointGrid.ItemsSource = referencePointTable.DefaultView;
            }
        }

        private void OnAddReferencePointClicked(object sender, RoutedEventArgs e)
        {
            var newRow = referencePointTable.NewRow();
            referencePointTable.Rows.Add(newRow);
        }

        private void OnDeleteReferencePointClicked(object sender, RoutedEventArgs e)
        {
            var selectedRow = referencePointGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                selectedRow.Row.Delete(); // Отмечаем строку для удаления из таблицы

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var adapter = new SqlDataAdapter("SELECT ID_RefPoint, Latitude, Longitude, Concentration FROM ReferencePoint", connection);

                    var builder = new SqlCommandBuilder(adapter);

                    adapter.Update(referencePointTable); // Удаляем данные из базы данных

                    MessageBox.Show("Changes saved successfully.");
                }
            }
        }

            private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var adapter = new SqlDataAdapter("SELECT Id, Name, Latitude, Longitude FROM ReferencePoint", connection);

                var builder = new SqlCommandBuilder(adapter);

                adapter.Update(referencePointTable);
            }
        }

        private void OnSaveReferencePointsClicked(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var adapter = new SqlDataAdapter("SELECT ID_RefPoint, Latitude, Longitude, Concentration FROM ReferencePoint", connection);

                var builder = new SqlCommandBuilder(adapter);

                adapter.Update(referencePointTable);

                MessageBox.Show("Changes saved successfully.");
            }
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            DBMain but = new DBMain();
            but.Show();
            this.Close();
        }
        private void referencePointGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}