using Internship_backend.Models;

namespace Internship_backend.Interfaces;

public interface IInterventionRepository
{
    IEnumerable<Interventions> GetAllInterventions();
    Interventions GetInterventionById(int id);
    Interventions CreateIntervention(Interventions intervention);
    void UpdateIntervention(Interventions intervention);
    void DeleteIntervention(int id);
}