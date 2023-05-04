using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class DriverComplaintService : IDriverComplaintService
    {
        private readonly IUnitOfWorkDriverComplaint _unitOfWork;

        public DriverComplaintService(IUnitOfWorkDriverComplaint unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DriverComplaint> GetAll()
        {
            return _unitOfWork.DriverComplaints.GetAll(item => item.IsDeleted == false);
        }

        public List<DriverComplaintDisplayDTO> GetDriverComplaints()
        {
            List<DriverComplaintDisplayDTO> driverComplaints = new List<DriverComplaintDisplayDTO>();

            List<DriverComplaint> existingComplaints = _unitOfWork.DriverComplaints.GetAll(item => item.IsDeleted == false);

            foreach (var complaint in existingComplaints)
            {
                DriverComplaintDisplayDTO newdisplayComplaint = new()
                {
                    Complaint = complaint.Complaint
                };

                driverComplaints.Add(newdisplayComplaint);
            }

            return driverComplaints;
        }

        public bool Add(DriverComplaintDTO newComplaint)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newComplaint.DriverName && item.IsDeleted == false);
            
            if (user == null) return false;

            Driver driver = _unitOfWork.Drivers.Get(item => item.UserId == user.Id && item.IsDeleted == false);

            user = _unitOfWork.Users.Get(item => item.UserName == newComplaint.UserName && item.IsDeleted == false);

            BookingStatus status = _unitOfWork.BookingsStatus.Get(item => item.Status == Status.Completed.ToString());

            Booking booking = _unitOfWork.Bookings.GetLast(user.Id,driver.Id,status.Id );


            if (user == null) return false;

            if(driver == null) return false;    

            DriverComplaint complaint = new()
            {
                Complaint = newComplaint.Complaint,
                DriverId = driver.Id,
                BookingId = booking.Id,
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.DriverComplaints.Add(complaint);
            _unitOfWork.Complete();

            return true;
        }
    }
}
