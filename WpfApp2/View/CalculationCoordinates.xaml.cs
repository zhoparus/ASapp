using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace WpfApp2.View
{
    /// <summary>
    /// Логика взаимодействия для CalculationCoordinates.xaml
    /// </summary>
    public partial class CalculationCoordinates : Window
    {
        // Connection string for the database
        private const string ConnectionString = @"Data Source=DESKTOP-R71RRJI\EVA;Initial Catalog=ArealSourceDB;Integrated Security=True;TrustServerCertificate=True";

        private readonly DataTable CalculationCoordinatesTable = new DataTable();

        public CalculationCoordinates()
        {
            InitializeComponent();

            LoadCalculationCoordinates();
        }

        private void LoadCalculationCoordinates()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var adapter = new SqlDataAdapter("SELECT TOP (1000) [ID_CalcCoord], [Latitude], [Longitude], [FK_ReferencePoint], [FK_SamplePointCoordinates] FROM [ArealSourceDB].[dbo].[CalculationCoordinates]", connection);

                adapter.Fill(CalculationCoordinatesTable);

                calculationCoordGrid.ItemsSource = CalculationCoordinatesTable.DefaultView;
            }
        }

        private void OnAddCalculationCoordClicked(object sender, RoutedEventArgs e)
        {
            var newRow = CalculationCoordinatesTable.NewRow();
            CalculationCoordinatesTable.Rows.Add(newRow);
        }

        private void OnDeleteCalculationCoordClicked(object sender, RoutedEventArgs e)
        {
            var selectedRow = calculationCoordGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                selectedRow.Row.Delete(); // Отмечаем строку для удаления из таблицы

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var adapter = new SqlDataAdapter("SELECT TOP (1000) [ID_CalcCoord], [Latitude], [Longitude], [FK_ReferencePoint], [FK_SamplePointCoordinates] FROM [ArealSourceDB].[dbo].[CalculationCoordinates]", connection);

                    var builder = new SqlCommandBuilder(adapter);

                    adapter.Update(CalculationCoordinatesTable); // Удаляем данные из базы данных

                    MessageBox.Show("Changes saved successfully.");
                }
            }
        }

        private void OnSaveCalculationCoordsClicked(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var adapter = new SqlDataAdapter("SELECT TOP (1000) [ID_CalcCoord], [Latitude], [Longitude], [FK_ReferencePoint], [FK_SamplePointCoordinates] FROM [ArealSourceDB].[dbo].[CalculationCoordinates]", connection);

                var builder = new SqlCommandBuilder(adapter);

                adapter.Update(CalculationCoordinatesTable);

                MessageBox.Show("Changes saved successfully.");
            }
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            DBMain but = new DBMain();
            but.Show();
            this.Close();
        }
        private void calculationCoordGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }
        }
    }