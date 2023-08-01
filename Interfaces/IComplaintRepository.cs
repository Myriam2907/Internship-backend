using Internship_backend.Models;


namespace Internship_backend.Interfaces;

public interface IComplaintRepository
{
    IEnumerable<Complaints> GetAllComplaints();
    Complaints GetComplaintById(int id);
    Complaints CreateComplaint(Complaints complaint);
    void UpdateComplaint(Complaints complaint);
    void DeleteComplaint(int id);
}