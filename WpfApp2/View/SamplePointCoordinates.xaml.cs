using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2.View
{
    /// <summary>
    /// Interaction logic for SamplePointCoordinates.xaml
    /// </summary>
    public partial class SamplePointCoordinates : Window
    {
        // Connection string for the database
        private const string ConnectionString = @"Data Source=DESKTOP-R71RRJI\EVA;Initial Catalog=ArealSourceDB;Integrated Security=True;TrustServerCertificate=True";

        private readonly DataTable SamplePointCoordinatesTable = new DataTable();

        public SamplePointCoordinates()
        {
            InitializeComponent();

            LoadSamplePoints();
        }

        private void LoadSamplePoints()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var adapter = new SqlDataAdapter("SELECT TOP (1000) [ID_SamlePointCoord], [Latitude], [Longitude], [Date] FROM [ArealSourceDB].[dbo].[SamplePointCoordinates]", connection);

                adapter.Fill(SamplePointCoordinatesTable);

                samplePointGrid.ItemsSource = SamplePointCoordinatesTable.DefaultView;
            }
        }

        private void OnAddSamplePointClicked(object sender, RoutedEventArgs e)
        {
            var newRow = SamplePointCoordinatesTable.NewRow();
            SamplePointCoordinatesTable.Rows.Add(newRow);
        }

        private void OnDeleteSamplePointClicked(object sender, RoutedEventArgs e)
        {
            var selectedRow = samplePointGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                selectedRow.Row.Delete(); // Отмечаем строку для удаления из таблицы

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var adapter = new SqlDataAdapter("SELECT TOP (1000) [ID_SamlePointCoord], [Latitude], [Longitude], [Date] FROM [ArealSourceDB].[dbo].[SamplePointCoordinates]", connection);

                    var builder = new SqlCommandBuilder(adapter);

                    adapter.Update(SamplePointCoordinatesTable); // Удаляем данные из базы данных

                    MessageBox.Show("Changes saved successfully.");
                }
            }
        }


        private void OnSaveSamplePointsClicked(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var adapter = new SqlDataAdapter("SELECT TOP (1000) [ID_SamlePointCoord], [Latitude], [Longitude], [Date] FROM [ArealSourceDB].[dbo].[SamplePointCoordinates]", connection);

                var builder = new SqlCommandBuilder(adapter);

                adapter.Update(SamplePointCoordinatesTable);

                MessageBox.Show("Changes saved successfully.");
            }
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            DBMain but = new DBMain();
            but.Show();
            this.Close();
        }

        private void samplePointGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}