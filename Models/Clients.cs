using System.ComponentModel.DataAnnotations;

namespace Internship_backend.Models;

public class Clients
{
    // Client ID
    [Key] public int ClientId { get; set; }

    //  ClientName
    [Required] [MaxLength(100)] public string ClientName { get; set; }

    // Client email
    [Required] [EmailAddress] public string Email { get; set; }

    //  Client phone
    [Required] [Phone] public string Phone { get; set; }
}