using System.ComponentModel.DataAnnotations;

namespace Internship_backend.Models;

public class Interventions
{
    [Key] public int InterventionId { get; set; }

    // Client information
    [Required] [MaxLength(100)] public string Client { get; set; }

    // Project information
    [Required] [MaxLength(100)] public string Project { get; set; }

    // Date of the intervention
    [Required] public DateTime Date { get; set; }

    // Intervention description
    [Required] [MaxLength(500)] public string Description { get; set; }
}