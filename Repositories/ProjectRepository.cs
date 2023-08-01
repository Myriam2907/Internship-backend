using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Internship_backend.Interfaces;
using Internship_backend.Models;

namespace Internship_backend.Repositories;

public class ProjectRepository : IProjectRepository
{
    private string connectionString;

    public ProjectRepository(string dbConnectionString)
    {
        // Set the connection string with the provided database connection string
        connectionString = dbConnectionString;
    }

    public IEnumerable<Projects> GetAllProjects()
    {
        List<Projects> projects = new();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Projects";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = Convert.ToInt32(reader["ProjectId"]);
                        string name = reader["ProjectName"].ToString();
                        // Add other columns as needed

                        var project = new Projects
                        {
                            ProjectId = id,
                            ProjectName = name
                            // Set other properties as needed
                        };

                        projects.Add(project);
                    }
                }
            }
        }

        return projects;
    }

    public Projects GetProjectById(int id)
    {
        Projects project = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Projects WHERE ProjectId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader["ProjectName"].ToString();
                        // Add other columns as needed

                        project = new Projects
                        {
                            ProjectId = id,
                            ProjectName = name
                            // Set other properties as needed
                        };
                    }
                }
            }
        }

        return project;
    }

    public Projects CreateProject(Projects project)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "INSERT INTO Projects (ProjectName) VALUES (@Name); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", project.ProjectName);

                var newId = Convert.ToInt32(command.ExecuteScalar());
                project.ProjectId = newId;
            }
        }

        return project;
    }

    public void UpdateProject(Projects project)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "UPDATE Projects SET ProjectName = @Name WHERE ProjectId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", project.ProjectName);
                command.Parameters.AddWithValue("@Id", project.ProjectId);

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteProject(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var query = "DELETE FROM Projects WHERE ProjectId = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }

        }
    }
}
