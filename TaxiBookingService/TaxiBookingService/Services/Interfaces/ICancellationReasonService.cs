using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface ICancellationReasonService
    {
        List<CancellationReason> GetAll();
        List<DisplayCancellationReasonsDTO> GetCancellationReasons();
        bool Add(CancellationReasonDTO newReason);
        bool Update(CancellationReasonDTO updatedReason, int id);
        bool Delete(int id);
    }
}
