using Internship_backend.Models;


namespace Internship_backend.Interfaces;

public interface IProjectRepository
{
    IEnumerable<Projects> GetAllProjects();
    Projects GetProjectById(int id);
    Projects CreateProject(Projects project);
    void UpdateProject(Projects project);
    void DeleteProject(int id);
}