using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class CancellationReasonService : ICancellationReasonService
    {
        private readonly IUnitOfWorkCancellationReason _unitOfWork;

        public CancellationReasonService(IUnitOfWorkCancellationReason unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CancellationReason> GetAll()
        {
            return _unitOfWork.CancellationReasons.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayCancellationReasonsDTO> GetCancellationReasons()
        {
            List<DisplayCancellationReasonsDTO> reasons = new List<DisplayCancellationReasonsDTO>();

            List<CancellationReason> existingReason = _unitOfWork.CancellationReasons.GetAll(item => item.IsDeleted == false);

            foreach (var status in existingReason)
            {
                DisplayCancellationReasonsDTO newdisplayReason = new()
                {
                    Reason = status.Reason,
                };

                reasons.Add(newdisplayReason);
            }

            return reasons;
        }

        public bool Add(CancellationReasonDTO newReason)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newReason.UserName && item.IsDeleted == false);
            bool reasonExists = _unitOfWork.CancellationReasons.Exists(item => item.Reason == newReason.Reason && item.IsDeleted == false);

            if (user == null) return false;

            if (reasonExists) return false;

            CancellationReason reason = new()
            {
                Reason = newReason.Reason,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.CancellationReasons.Add(reason);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(CancellationReasonDTO updatedReason, int id)
        {
            CancellationReason reason = _unitOfWork.CancellationReasons.Get(item => item.Id == id && item.IsDeleted == false);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedReason.UserName && item.IsDeleted == false);

            if (reason == null) return false;

            reason.Reason = updatedReason.Reason;
            reason.ModifiedAt = DateTime.Now;
            reason.ModifiedBy = user.Id;

            _unitOfWork.CancellationReasons.Update(reason);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            CancellationReason reason = _unitOfWork.CancellationReasons.Get(item => item.Id == id);

            if (reason == null) return false;
            if (reason.IsDeleted) return false;

            reason.IsDeleted = true;

            _unitOfWork.CancellationReasons.Update(reason);
            _unitOfWork.Complete();

            return true;
        }
    }
}
