using Internship_backend.Models;
using Internship_backend.Interfaces;


namespace Internship_backend.Interfaces;

public interface IClientRepository
{
    IEnumerable<Clients> GetAllClients();
    Clients GetClientById(int id);
    Clients CreateClient(Clients client);
    void UpdateClient(Clients client);
    void DeleteClient(int id);
}