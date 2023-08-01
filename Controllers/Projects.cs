using Microsoft.AspNetCore.Mvc;
using Internship_backend.Models;
using Internship_backend.Interfaces;

namespace Internship_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    // GET: api/projects
    [HttpGet]
    public IActionResult GetProjects()
    {
        var projects = _projectRepository.GetAllProjects();
        return Ok(projects);
    }

    // GET: api/projects/5
    [HttpGet("{id}")]
    public IActionResult GetProject(int id)
    {
        var project = _projectRepository.GetProjectById(id);

        if (project == null)
            return NotFound();

        return Ok(project);
    }

    // POST: api/projects
    [HttpPost]
    public IActionResult CreateProject([FromBody] Projects project)
    {
        if (project == null)
            return BadRequest();

        var createdProject = _projectRepository.CreateProject(project);
        return CreatedAtAction(nameof(GetProject), new { id = createdProject.ProjectId }, createdProject);
    }

    // PUT: api/projects/5
    [HttpPut("{id}")]
    public IActionResult UpdateProject(int id, [FromBody] Projects project)
    {
        if (project == null || id != project.ProjectId)
            return BadRequest();

        var existingProject = _projectRepository.GetProjectById(id);

        if (existingProject == null)
            return NotFound();

        _projectRepository.UpdateProject(project);
        return NoContent();
    }

    // DELETE: api/projects/5
    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        var projectToDelete = _projectRepository.GetProjectById(id);

        if (projectToDelete == null)
            return NotFound();

        _projectRepository.DeleteProject(id);
        return NoContent();
    }
}