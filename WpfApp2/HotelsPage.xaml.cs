﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {
        public HotelsPage()
        {
            InitializeComponent();
            //DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotels.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Hotel));
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridHotels.SelectedItems.Cast<Hotel>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ToursBaseEntities.GetContext().Hotel.RemoveRange(hotelsForRemoving);
                    ToursBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotel.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToursBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotel.ToList();
            }
        }
    }
}
