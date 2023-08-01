using Microsoft.AspNetCore.Mvc;
using Internship_backend.Models;
using Internship_backend.Interfaces;

namespace Internship_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintsController : ControllerBase
{
    private readonly IComplaintRepository _complaintRepository;

    public ComplaintsController(IComplaintRepository complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }

    // GET: api/complaints
    [HttpGet]
    public IActionResult GetComplaints()
    {
        var complaints = _complaintRepository.GetAllComplaints();
        return Ok(complaints);
    }

    // GET: api/complaints/5
    [HttpGet("{id}")]
    public IActionResult GetComplaint(int id)
    {
        var complaint = _complaintRepository.GetComplaintById(id);

        if (complaint == null)
            return NotFound();

        return Ok(complaint);
    }

    // POST: api/complaints
    [HttpPost]
    public IActionResult CreateComplaint([FromBody] Complaints complaint)
    {
        if (complaint == null)
            return BadRequest();

        var createdComplaint = _complaintRepository.CreateComplaint(complaint);
        return CreatedAtAction(nameof(GetComplaint), new { id = createdComplaint.ComplaintId }, createdComplaint);
    }

    // PUT: api/complaints/5
    [HttpPut("{id}")]
    public IActionResult UpdateComplaint(int id, [FromBody] Complaints complaint)
    {
        if (complaint == null || id != complaint.ComplaintId)
            return BadRequest();

        var existingComplaint = _complaintRepository.GetComplaintById(id);

        if (existingComplaint == null)
            return NotFound();

        _complaintRepository.UpdateComplaint(complaint);
        return NoContent();
    }

    // DELETE: api/complaints/5
    [HttpDelete("{id}")]
    public IActionResult DeleteComplaint(int id)
    {
        var complaintToDelete = _complaintRepository.GetComplaintById(id);

        if (complaintToDelete == null)
            return NotFound();

        _complaintRepository.DeleteComplaint(id);
        return NoContent();
    }
}