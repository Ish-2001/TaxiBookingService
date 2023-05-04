using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IDriverComplaintService
    {
        List<DriverComplaint> GetAll();
        List<DriverComplaintDisplayDTO> GetDriverComplaints();
        bool Add(DriverComplaintDTO newComplaint);
    }
}
