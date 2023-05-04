using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class PaymentModeService : IPaymentModeService
    {
        private readonly IUnitOfWorkPaymentMode _unitOfWork;

        public PaymentModeService(IUnitOfWorkPaymentMode unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<PaymentMode> GetAll()
        {
            return _unitOfWork.PaymentModes.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayPaymentModeDTO> GetPaymentModes()
        {
            List<DisplayPaymentModeDTO> modes = new List<DisplayPaymentModeDTO>();

            List<PaymentMode> existingModes = _unitOfWork.PaymentModes.GetAll(item => item.IsDeleted == false);

            foreach (var mode in existingModes)
            {
                DisplayPaymentModeDTO newdisplayMode = new()
                {
                    Mode = mode.Mode,
                };

                modes.Add(newdisplayMode);
            }

            return modes;
        }

        public bool Add(PaymentModeDTO newMode)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newMode.UserName && item.IsDeleted == false);
            bool modeExists = _unitOfWork.PaymentModes.Exists(item => item.Mode == newMode.Mode && item.IsDeleted == false);

            if (user == null) return false;

            if (modeExists) return false;

            PaymentMode mode = new()
            {
                Mode = newMode.Mode,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.PaymentModes.Add(mode);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(PaymentModeDTO updatedMode, int id)
        {
            PaymentMode mode = _unitOfWork.PaymentModes.Get(item => item.Id == id);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedMode.UserName);

            if (mode == null) return false;

            mode.Mode = updatedMode.Mode;
            mode.ModifiedAt = DateTime.Now;
            mode.ModifiedBy = user.Id;

            _unitOfWork.PaymentModes.Update(mode);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            PaymentMode mode = _unitOfWork.PaymentModes.Get(item => item.Id == id);

            if (mode == null) return false;
            if (mode.IsDeleted) return false;

            mode.IsDeleted = true;

            _unitOfWork.PaymentModes.Update(mode);
            _unitOfWork.Complete();

            return true;
        }
    }
}
