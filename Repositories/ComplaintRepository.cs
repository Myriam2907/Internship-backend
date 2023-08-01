using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Internship_backend.Interfaces;
using Internship_backend.Models;

namespace Internship_backend.Repositories;

public class ComplaintRepository : IComplaintRepository
{
    private string connectionString;

    public ComplaintRepository(string dbConnectionString)
    {
        // Set the connection string with the provided database connection string
        connectionString = dbConnectionString;
    }

    public IEnumerable<Complaints> GetAllComplaints()
    {
        List<Complaints> complaints = new();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Complaints";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = Convert.ToInt32(reader["ComplaintId"]);
                        string client = reader["Client"].ToString();
                        string project = reader["Project"].ToString();
                        string description = reader["Description"].ToString();
                        // Add other columns as needed

                        var complaint = new Complaints
                        {
                            ComplaintId = id,
                            Client = client,
                            Project = project,
                            Description = description
                            // Set other properties as needed
                        };

                        complaints.Add(complaint);
                    }
                }
            }
        }

        return complaints;
    }

    public Complaints GetComplaintById(int id)
    {
        Complaints complaint = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Complaints WHERE ComplaintId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string client = reader["Client"].ToString();
                        string project = reader["Project"].ToString();
                        string description = reader["Description"].ToString();
                        // Add other columns as needed

                        complaint = new Complaints
                        {
                            ComplaintId = id,
                            Client = client,
                            Project = project,
                            Description = description
                            // Set other properties as needed
                        };
                    }
                }
            }
        }

        return complaint;
    }

    public Complaints CreateComplaint(Complaints complaint)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query =
                "INSERT INTO Complaints (Client, Project, Description) VALUES (@Client, @Project, @Description); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Client", complaint.Client);
                command.Parameters.AddWithValue("@Project", complaint.Project);
                command.Parameters.AddWithValue("@Description", complaint.Description);

                var newId = Convert.ToInt32(command.ExecuteScalar());
                complaint.ComplaintId = newId;
            }
        }

        return complaint;
    }

    public void UpdateComplaint(Complaints complaint)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query =
                "UPDATE Complaints SET Client = @Client, Project = @Project, Description = @Description WHERE ComplaintId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Client", complaint.Client);
                command.Parameters.AddWithValue("@Project", complaint.Project);
                command.Parameters.AddWithValue("@Description", complaint.Description);
                command.Parameters.AddWithValue("@Id", complaint.ComplaintId);

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteComplaint(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "DELETE FROM Complaints WHERE ComplaintId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}