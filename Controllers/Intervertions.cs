using Microsoft.AspNetCore.Mvc;
using Internship_backend.Models;
using Internship_backend.Interfaces;

namespace Internship_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterventionsController : ControllerBase
{
    private readonly IInterventionRepository _interventionRepository;

    public InterventionsController(IInterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
    }

    // GET: api/interventions
    [HttpGet]
    public IActionResult GetInterventions()
    {
        var interventions = _interventionRepository.GetAllInterventions();
        return Ok(interventions);
    }

    // GET: api/interventions/5
    [HttpGet("{id}")]
    public IActionResult GetIntervention(int id)
    {
        var intervention = _interventionRepository.GetInterventionById(id);

        if (intervention == null)
            return NotFound();

        return Ok(intervention);
    }

    // POST: api/interventions
    [HttpPost]
    public IActionResult CreateIntervention([FromBody] Interventions intervention)
    {
        if (intervention == null)
            return BadRequest();

        var createdIntervention = _interventionRepository.CreateIntervention(intervention);
        return CreatedAtAction(nameof(GetIntervention), new { id = createdIntervention.InterventionId },
            createdIntervention);
    }

    // PUT: api/interventions/5
    [HttpPut("{id}")]
    public IActionResult UpdateIntervention(int id, [FromBody] Interventions intervention)
    {
        if (intervention == null || id != intervention.InterventionId)
            return BadRequest();

        var existingIntervention = _interventionRepository.GetInterventionById(id);

        if (existingIntervention == null)
            return NotFound();

        _interventionRepository.UpdateIntervention(intervention);
        return NoContent();
    }

    // DELETE: api/interventions/5
    [HttpDelete("{id}")]
    public IActionResult DeleteIntervention(int id)
    {
        var interventionToDelete = _interventionRepository.GetInterventionById(id);

        if (interventionToDelete == null)
            return NotFound();

        _interventionRepository.DeleteIntervention(id);
        return NoContent();
    }
}