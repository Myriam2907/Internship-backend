using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Internship_backend.Interfaces;
using Internship_backend.Models;

namespace Internship_backend.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string connectionString;

        public ClientRepository(string dbConnectionString)
        {
            // Set the connection string with the provided database connection string
            connectionString = dbConnectionString;
        }

        public IEnumerable<Clients> GetAllClients()
        {
            List<Clients> clients = new List<Clients>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Clients";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = Convert.ToInt32(reader["ClientId"]);
                            string name = reader["ClientName"].ToString();
                            string email = reader["Email"].ToString();
                            string phone = reader["Phone"].ToString();
                            // Add other columns as needed

                            var client = new Clients
                            {
                                ClientId = id,
                                ClientName = name,
                                Email = email,
                                Phone = phone
                                // Set other properties as needed
                            };

                            clients.Add(client);
                        }
                    }
                }
            }

            return clients;
        }

        public Clients GetClientById(int id)
        {
            Clients client = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Clients WHERE ClientId = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader["ClientName"].ToString();
                            string email = reader["Email"].ToString();
                            string phone = reader["Phone"].ToString();
                            // Add other columns as needed

                            client = new Clients
                            {
                                ClientId = id,
                                ClientName = name,
                                Email = email,
                                Phone = phone
                                // Set other properties as needed
                            };
                        }
                    }
                }
            }

            return client;
        }

        public Clients CreateClient(Clients client)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query =
                    "INSERT INTO Clients (ClientName, Email, Phone) VALUES (@Name, @Email, @Phone); SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", client.ClientName);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@Phone", client.Phone);

                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    client.ClientId = newId;
                }
            }

            return client;
        }

        public void UpdateClient(Clients client)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "UPDATE Clients SET ClientName = @Name, Email = @Email, Phone = @Phone WHERE ClientId = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", client.ClientName);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@Phone", client.Phone);
                    command.Parameters.AddWithValue("@Id", client.ClientId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteClient(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "DELETE FROM Clients WHERE ClientId = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
