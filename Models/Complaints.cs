using System.ComponentModel.DataAnnotations;

namespace Internship_backend.Models;

public class Complaints
{
    [Key] public int ComplaintId { get; set; }

    // Client information
    [Required] [MaxLength(100)] public string Client { get; set; }

    // Project information
    [Required] [MaxLength(100)] public string Project { get; set; }

    // Complaint description
    [Required] [MaxLength(500)] public string Description { get; set; }
}