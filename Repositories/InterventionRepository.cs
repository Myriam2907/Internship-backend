using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Internship_backend.Interfaces;
using Internship_backend.Models;

namespace Internship_backend.Repositories;

public class InterventionRepository : IInterventionRepository
{
    private string connectionString;

    public InterventionRepository(string dbConnectionString)
    {
        connectionString = dbConnectionString;
    }

    public IEnumerable<Interventions> GetAllInterventions()
    {
        List<Interventions> interventions = new();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Interventions";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = Convert.ToInt32(reader["InterventionId"]);
                        string client = reader["Client"].ToString();
                        string project = reader["Project"].ToString();
                        var date = Convert.ToDateTime(reader["Date"]);
                        string description = reader["Description"].ToString();
                        // Add other columns as needed

                        var intervention = new Interventions
                        {
                            InterventionId = id,
                            Client = client,
                            Project = project,
                            Date = date,
                            Description = description
                            // Set other properties as needed
                        };

                        interventions.Add(intervention);
                    }
                }
            }
        }

        return interventions;
    }

    public Interventions GetInterventionById(int id)
    {
        Interventions intervention = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Interventions WHERE InterventionId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string client = reader["Client"].ToString();
                        string project = reader["Project"].ToString();
                        var date = Convert.ToDateTime(reader["Date"]);
                        string description = reader["Description"].ToString();
                        // Add other columns as needed

                        intervention = new Interventions
                        {
                            InterventionId = id,
                            Client = client,
                            Project = project,
                            Date = date,
                            Description = description
                            // Set other properties as needed
                        };
                    }
                }
            }
        }

        return intervention;
    }

    public Interventions CreateIntervention(Interventions intervention)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query =
                "INSERT INTO Interventions (Client, Project, Date, Description) VALUES (@Client, @Project, @Date, @Description); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Client", intervention.Client);
                command.Parameters.AddWithValue("@Project", intervention.Project);
                command.Parameters.AddWithValue("@Date", intervention.Date);
                command.Parameters.AddWithValue("@Description", intervention.Description);

                var newId = Convert.ToInt32(command.ExecuteScalar());
                intervention.InterventionId = newId;
            }
        }

        return intervention;
    }

    public void UpdateIntervention(Interventions intervention)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query =
                "UPDATE Interventions SET Client = @Client, Project = @Project, Date = @Date, Description = @Description WHERE InterventionId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Client", intervention.Client);
                command.Parameters.AddWithValue("@Project", intervention.Project);
                command.Parameters.AddWithValue("@Date", intervention.Date);
                command.Parameters.AddWithValue("@Description", intervention.Description);
                command.Parameters.AddWithValue("@Id", intervention.InterventionId);

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteIntervention(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "DELETE FROM Interventions WHERE InterventionId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }

        }
    }
}