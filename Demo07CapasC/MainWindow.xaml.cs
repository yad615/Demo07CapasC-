using System;
using System.Windows;
using System.Windows.Controls;
using Business;
using Entity;

namespace Demo07CapasC
{
    public partial class MainWindow : Window
    {
        private Customer clienteSeleccionado;
        private BCustomer businessCustomer = new BCustomer();

        public MainWindow()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void CargarClientes()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = businessCustomer.Read();
        }

        private void LimpiarFormulario()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            clienteSeleccionado = null;
            dataGrid.SelectedItem = null;
            btnEliminar.IsEnabled = false;
            btnGuardar.Content = "Registrar Cliente";
            txtName.Focus();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clienteSeleccionado = dataGrid.SelectedItem as Customer;

            if (clienteSeleccionado != null)
            {
                txtName.Text = clienteSeleccionado.Name;
                txtAddress.Text = clienteSeleccionado.Address;
                txtPhone.Text = clienteSeleccionado.Phone;

                btnGuardar.Content = "Actualizar Cambios";
                btnEliminar.IsEnabled = true;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clienteSeleccionado == null)
                {
                    businessCustomer.Create(new Customer
                    {
                        Name = txtName.Text,
                        Address = txtAddress.Text,
                        Phone = txtPhone.Text
                    });
                    MessageBox.Show("Cliente registrado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    clienteSeleccionado.Name = txtName.Text;
                    clienteSeleccionado.Address = txtAddress.Text;
                    clienteSeleccionado.Phone = txtPhone.Text;
                    businessCustomer.Update(clienteSeleccionado);
                    MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                CargarClientes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (clienteSeleccionado != null)
            {
                var resultado = MessageBox.Show($"¿Estás segura de que deseas eliminar a {clienteSeleccionado.Name}?",
                                                 "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        businessCustomer.Delete(clienteSeleccionado.CustomerId);
                        MessageBox.Show("Cliente eliminado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarClientes();
                        LimpiarFormulario();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error Inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        private void txtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = txtBuscador.Text;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                CargarClientes();
            }
            else
            {
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = businessCustomer.Search(searchTerm);
            }
        }
    }
}
