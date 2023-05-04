using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class BookingStatusService : IBookingStatusService
    {
        private readonly IUnitOfWorkBookingStatus _unitOfWork;

        public BookingStatusService(IUnitOfWorkBookingStatus unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<BookingStatus> GetAll()
        {
            return _unitOfWork.BookingsStatus.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayBookingStatusDTO> GetBookingStatus()
        {
            List<DisplayBookingStatusDTO> statuses = new List<DisplayBookingStatusDTO>();

            List<BookingStatus> existingStatus = _unitOfWork.BookingsStatus.GetAll(item => item.IsDeleted == false);

            foreach (var status in existingStatus)
            {
                DisplayBookingStatusDTO newdisplayStatus = new()
                {
                    Status = status.Status,
                };

                statuses.Add(newdisplayStatus);
            }

            return statuses;
        }

        public bool Add(BookingStatusDTO newStatus)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newStatus.UserName && item.IsDeleted == false);
            bool statusExists = _unitOfWork.BookingsStatus.Exists(item => item.Status == newStatus.Status && item.IsDeleted == false);

            if (user == null) return false;

            if (statusExists) return false;

            BookingStatus status = new()
            {
                Status = newStatus.Status,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.BookingsStatus.Add(status);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(BookingStatusDTO updatedStatus, int id)
        {
            BookingStatus status = _unitOfWork.BookingsStatus.Get(item => item.Id == id && item.IsDeleted == false);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedStatus.UserName && item.IsDeleted == false);

            if (status == null) return false;

            status.Status = updatedStatus.Status;
            status.ModifiedAt = DateTime.Now;
            status.ModifiedBy = user.Id;

            _unitOfWork.BookingsStatus.Update(status);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            BookingStatus status = _unitOfWork.BookingsStatus.Get(item => item.Id == id);

            if (status == null) return false;
            if (status.IsDeleted) return false;

            status.IsDeleted = true;

            _unitOfWork.BookingsStatus.Update(status);
            _unitOfWork.Complete();

            return true;
        }
    }
}
