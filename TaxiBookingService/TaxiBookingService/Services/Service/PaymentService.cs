using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWorkPayment _unitOfWork;

        public PaymentService(IUnitOfWorkPayment unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Payment> GetAll()
        {
            return _unitOfWork.Payments.GetAll(item => item.IsDeleted == false);
        }


        public bool Add(Booking booking)
        {
            User user = _unitOfWork.Users.Get(item => item.Id == booking.UserId && item.IsDeleted == false);

            if (user == null) return false;

            Payment payment = new()
            {
                Date = booking.Date,
                Amount = (int)booking.RideFare,
                CreatedAt = DateTime.Now,
                CreatedBy = user.Id,
                IsPending = true,
                BookingId = booking.Id,
                ModeId = (int)booking.ModeId
            };

            _unitOfWork.Payments.Add(payment);
            _unitOfWork.Complete();
            return true;
        }
    }
}
