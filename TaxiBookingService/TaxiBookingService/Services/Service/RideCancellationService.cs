using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class RideCancellationService : IRideCancellationService
    {
        private readonly IUnitOfWorkRideCancellation _unitOfWork;

        public RideCancellationService(IUnitOfWorkRideCancellation unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<RideCancellation> GetAll()
        {
            return _unitOfWork.RideCancellations.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayRideCancellationDTO> GetPaymentModes()
        {
            List<DisplayRideCancellationDTO> displayRideCancellations = new List<DisplayRideCancellationDTO>();

            List<RideCancellation> rideCancellations = _unitOfWork.RideCancellations.GetAll();

            foreach (var rideCancellation in rideCancellations)
            {
                DisplayRideCancellationDTO newdisplayMode = new()
                {
                    CancellationFee= rideCancellation.CancellationFee,
                    Reason = rideCancellation.CancellationReason.Reason,
                    UserName = rideCancellation.User.FirstName + " " + rideCancellation.User.LastName
                };

                displayRideCancellations.Add(newdisplayMode);
            }

            return displayRideCancellations;
        }

        public bool Add(RideCancellationDTO rideCancellationDTO)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == rideCancellationDTO.UserName && item.IsDeleted == false);
            BookingStatus status = _unitOfWork.BookingsStatus.Get(item => item.Status == Status.Cancelled.ToString() && item.IsDeleted == false);
            Booking booking = _unitOfWork.Bookings.Get(item => item.Id == rideCancellationDTO.BookingId && item.IsDeleted == false);
            Payment payment = _unitOfWork.Payments.Get(item => item.BookingId == booking.Id && item.IsDeleted == false);
            CancellationReason reason = _unitOfWork.CancellationReasons.Get(item => item.Reason == rideCancellationDTO.Reason && item.IsDeleted == false);


            if (user == null) return false;
            if (booking == null) return false;
            if (payment == null) return false;

            RideCancellation rideCancellation = new()
            {
                CancellationFee = payment.Amount * 15 / 100,
                ReasonId = reason.Id,
                BookingId = booking.Id,
                UserId = user.Id,
                IsPending = true,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                CreatedBy = user.Id
            };

            booking.StatusId = status.Id;

            _unitOfWork.Bookings.Update(booking);
            _unitOfWork.RideCancellations.Add(rideCancellation);
            _unitOfWork.Complete();
            return true;
        }


        public bool PayCancellationFee(string userName)
        {
            List<RideCancellation> rideCancellations = new List<RideCancellation>();
            rideCancellations = _unitOfWork.RideCancellations.GetAll().FindAll(e => e.User.UserName == userName && e.IsPending == true);

            foreach(var ride in rideCancellations)
            {
                ride.IsPending= false;
                _unitOfWork.RideCancellations.Update(ride);
            }
            _unitOfWork.Complete();
            return true;
        }
    }
}
