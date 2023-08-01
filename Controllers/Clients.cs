using Microsoft.AspNetCore.Mvc;
using Internship_backend.Models;
using Internship_backend.Interfaces;

namespace Internship_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    // Your data repository or service to interact with the database
    private readonly IClientRepository _clientRepository;

    public ClientsController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    // GET: api/clients
    [HttpGet]
    public IActionResult GetClients()
    {
        var clients = _clientRepository.GetAllClients();
        return Ok(clients);
    }

    // GET: api/clients/5
    [HttpGet("{id}")]
    public IActionResult GetClient(int id)
    {
        var client = _clientRepository.GetClientById(id);

        if (client == null)
            return NotFound();

        return Ok(client);
    }

    // POST: api/clients
    [HttpPost]
    public IActionResult CreateClient([FromBody] Clients client)
    {
        if (client == null)
            return BadRequest();

        var createdClient = _clientRepository.CreateClient(client);
        return CreatedAtAction(nameof(GetClient), new { id = createdClient.ClientId }, createdClient);
    }

    // PUT: api/clients/5
    [HttpPut("{id}")]
    public IActionResult UpdateClient(int id, [FromBody] Clients client)
    {
        if (client == null || id != client.ClientId)
            return BadRequest();

        var existingClient = _clientRepository.GetClientById(id);

        if (existingClient == null)
            return NotFound();

        _clientRepository.UpdateClient(client);
        return NoContent();
    }

    // DELETE: api/clients/5
    [HttpDelete("{id}")]
    public IActionResult DeleteClient(int id)
    {
        var clientToDelete = _clientRepository.GetClientById(id);

        if (clientToDelete == null)
            return NotFound();

        _clientRepository.DeleteClient(id);
        return NoContent();
    }
}