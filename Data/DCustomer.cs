using System.Collections.Generic;
using System.Data;
using Entity;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class DCustomer
    {
        private string _connectionString = "Server=localhost\\SQLEXPRESS;Database=InvoicesCDB;User ID=alcantaraTecsup;Password=Tecsup2020;TrustServerCertificate=true;";

        public List<Customer> Read()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_ListarClientes", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = (int)reader["customer_id"],
                            Name = reader["name"].ToString(),
                            Address = reader["address"].ToString(),
                            Phone = reader["phone"].ToString(),
                            Active = (bool)reader["active"]
                        });
                    }
                }
            }
            return customers;
        }

        public void Create(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_InsertarCliente", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_ActualizarCliente", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_EliminarCliente", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CustomerId", customerId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Customer> Search(string searchTerm)
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_BuscarClientes", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SearchTerm", searchTerm);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = (int)reader["customer_id"],
                            Name = reader["name"].ToString(),
                            Address = reader["address"].ToString(),
                            Phone = reader["phone"].ToString(),
                            Active = (bool)reader["active"]
                        });
                    }
                }
            }
            return customers;
        }
    }
}
