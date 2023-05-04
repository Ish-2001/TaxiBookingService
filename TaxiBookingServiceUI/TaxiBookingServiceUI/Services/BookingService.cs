using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AssetManagementSystemUI;
using TaxiBookingServiceUI.UnitOfWork;

namespace TaxiBookingServiceUI.Services
{
    public class BookingService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }
        private string _drivertoken { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly UnitOfWorkBooking _unitOfWork;

        public BookingService(IHttpContextAccessor httpContextAccessor, UnitOfWorkBooking unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("usertoken");
            _drivertoken = _session.GetString("drivertoken");
            _unitOfWork = unitOfWork;

        }

        public double CalculateDistance(double pickupLatitude, double pickupLongitude, double dropLatitude, double dropLongitude)
        {
            const double earthRadius = 6371; 
            var latitudeDifference = ToRadians(dropLatitude - pickupLatitude);
            var longitudeDifference = ToRadians(dropLongitude - pickupLongitude);
            var a = Math.Sin(latitudeDifference / 2) * Math.Sin(latitudeDifference / 2) +
                    Math.Cos(ToRadians(pickupLatitude)) * Math.Cos(ToRadians(dropLatitude)) *
                    Math.Sin(longitudeDifference / 2) * Math.Sin(longitudeDifference / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = earthRadius * c;
            return distance;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public async Task<double> CalculateLocation(BookingViewModel booking)
        {
            double pickupLatitude = 0, pickupLongitude = 0, dropLatitude = 0, dropLongitude = 0;
            string address = booking.PickupLocation;

            PositionViewModel position = await PositionService.CalculateLocation(address);

            pickupLatitude = position.Latitude;
            pickupLongitude = position.Longitude;

            address = booking.Destination;

            position = await PositionService.CalculateLocation(address);

            dropLatitude = position.Latitude;
            dropLongitude = position.Longitude;

            var distance = CalculateDistance(pickupLatitude, pickupLongitude, dropLatitude, dropLongitude);
            return distance;
        }

        

        public HttpResponseMessage BookingDetails(BookingViewModel booking, string userName)
        {

            UserViewModel user = _unitOfWork.user.GetUsers().Result.Where(item => item.UserName == userName).FirstOrDefault();

            BookingViewModel requestedBooking = new()
            {
                PickupLocation = booking.PickupLocation,
                Destination = booking.Destination,
                UserName = userName,
                Date = booking.Date,
                RideFare = booking.RideFare,
                RideTime = booking.RideTime,
                Status = Status.Requested.ToString(),
                PaymentMode = booking.PaymentMode
            };

            HttpResponseMessage message = _helper.PostAsJsonAsync(ApiUrls.BookingAdd, requestedBooking , _token);

            booking.Status = Status.Requested.ToString();
            booking.UserName = requestedBooking.UserName;

            return message;
        }

        public async Task<BookingViewModel> BookingRequest(BookingViewModel booking)
        { 
            double distance = await CalculateLocation(booking);

            if (distance < 3)
                booking.RideFare = 50;

            booking.RideFare = (decimal)_unitOfWork.rideCancellation.CalculateRideFare(distance);

            double speed = 60.0f;

            booking.RideTime = (decimal)(60 *(distance / speed));

            return booking;
        }

        public async Task<List<BookingViewModel>> GetAll()
        {
            List<BookingViewModel> bookings = new List<BookingViewModel>();

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.BookingDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                bookings = JsonConvert.DeserializeObject<List<BookingViewModel>>(result);
            }

            foreach (var booking in bookings)
            {
                booking.Status = _unitOfWork.status.GetAll().Result.Where(item => item.Id == booking.StatusId)
                                 .Select(item => item.Status).FirstOrDefault();
                booking.Name = _unitOfWork.user.GetUsers().Result.Where(item => item.Id.Equals(booking.UserId))
                             .Select(item => item.FirstName).FirstOrDefault() + " " +
                             _unitOfWork.user.GetUsers().Result.Where(item => item.Id.Equals(booking.UserId))
                             .Select(item => item.LastName).FirstOrDefault();

                booking.PaymentMode = _unitOfWork.paymentMode.GetAll().Result.Where(item => item.Id == booking.ModeId).
                                      Select(item => item.Mode).FirstOrDefault();

            }


            return bookings;
        }

        public HttpResponseMessage AcceptRequest(int id , string userName)
        {
            BookingViewModel updatedBooking = GetAll().Result.Where(item => item.Id == id).FirstOrDefault();
            
            BookingViewModel booking = new()
            {
                PickupLocation = updatedBooking.PickupLocation,
                Date = updatedBooking.Date,
                Destination= updatedBooking.Destination,
                RideFare= updatedBooking.RideFare,
                RideTime= updatedBooking.RideTime,
                Status = Status.Booked.ToString(),
                
                UserName = userName              
            };

            var message = _helper.PutAsJsonAsync(ApiUrls.BookingUpdate, booking, id, _token);

            return message;
        }

        public int GetBookingId(BookingViewModel booking)
        {
            int userId = _unitOfWork.user.GetUsers().Result.Where(item => item.UserName == booking.UserName)
                         .Select(item => item.Id).FirstOrDefault();
            int bookingId = GetAll().Result
                      .Where(item => item.UserId == userId && item.Status != Status.Completed.ToString() && 
                      item.Status != Status.Cancelled.ToString() && item.Status != Status.Arrived.ToString()).Select(item => item.Id)
                      .FirstOrDefault();
            return bookingId;
        }

        public BookingViewModel GetBookingById(int bookingId)
        {
            BookingViewModel booking = GetAll().Result
                      .Where(item => item.Id == bookingId).FirstOrDefault();

            return booking;
        }

       public List<BookingViewModel> GetAcceptedRequest(int id)
        {
          
            return GetAll().Result.Where(Item => Item.Id == id).ToList();
        }

        public HttpResponseMessage CancelRequest(int id , string userName)
        {

            BookingViewModel updatedBooking = GetAll().Result.Where(item => item.Id == id).FirstOrDefault();

            BookingViewModel booking = new()
            {
                PickupLocation = updatedBooking.PickupLocation,
                Date = updatedBooking.Date,
                Destination = updatedBooking.Destination,
                RideFare = updatedBooking.RideFare,
                RideTime = updatedBooking.RideTime,
                Status = Status.Cancelled.ToString(),
                UserName = userName
            };

            var message = _helper.PutAsJsonAsync(ApiUrls.BookingUpdate, booking, id, _token);

            return message;

        }
        

        public HttpResponseMessage CompleteRide(int id, string userName)
        { 

            BookingViewModel updatedBooking = GetAll().Result.Where(item => item.Id == id).FirstOrDefault();

            BookingViewModel booking = new()
            {
                PickupLocation = updatedBooking.PickupLocation,
                Date = updatedBooking.Date,
                Destination = updatedBooking.Destination,
                RideFare = updatedBooking.RideFare,
                RideTime = updatedBooking.RideTime,
                Status = Status.Completed.ToString(),
                UserName = userName
            };

            var message = _helper.PutAsJsonAsync(ApiUrls.BookingUpdate,booking,id,_token);

            return message;

        }

        public HttpResponseMessage Arrived(int id, string userName)
        {

            BookingViewModel updatedBooking = GetAll().Result.Where(item => item.Id == id).FirstOrDefault();

            BookingViewModel booking = new()
            {
                PickupLocation = updatedBooking.PickupLocation,
                Date = updatedBooking.Date,
                Destination = updatedBooking.Destination,
                RideFare = updatedBooking.RideFare,
                RideTime = updatedBooking.RideTime,
                Status = Status.Arrived.ToString(),
                UserName = userName
            };

            var message = _helper.PutAsJsonAsync(ApiUrls.BookingUpdate, booking, id, _token);

            return message;

        }

        public HttpResponseMessage StartRide(int id, string userName)
        {
            BookingViewModel updatedBooking = GetAll().Result.Where(item => item.Id == id).FirstOrDefault();

            BookingViewModel booking = new()
            {
                PickupLocation = updatedBooking.PickupLocation,
                Date = updatedBooking.Date,
                Destination = updatedBooking.Destination,
                RideFare = updatedBooking.RideFare,
                RideTime = updatedBooking.RideTime,
                Status = Status.Started.ToString(),
                UserName = userName
            };

            var message = _helper.PutAsJsonAsync(ApiUrls.BookingUpdate, booking, id, _token);

            return message;

        }

        public HttpResponseMessage CompletePayment(int id)
        { 
            PaymentViewModel updatedPayment = _unitOfWork.payment.GetAll().Result.Where(item => item.BookingId == id).FirstOrDefault();

            PaymentViewModel payment = new()
            {
               Amount = updatedPayment.Amount,
               BookingId = updatedPayment.BookingId,
               Date = updatedPayment.Date,
               ModeId = updatedPayment.ModeId
            };

            var message = _helper.PutAsJsonAsync(ApiUrls.PaymentUpdate,payment , id,_drivertoken);

            return message;

        }

        public HttpResponseMessage Add(RideCancellationViewModel rideCancellation, string userName)
        {
            int userId = _unitOfWork.user.GetUsers().Result.Where(item => item.UserName == userName).Select(item => item.Id).FirstOrDefault();
            int statusId = _unitOfWork.status.GetAll().Result.Where(item => item.Status == Status.Booked.ToString()).Select(item => item.Id)
                           .FirstOrDefault();
            int bookingId = GetAll().Result.Where(item => item.UserId == userId && item.StatusId == statusId)
                             .Select(item => item.Id).FirstOrDefault();


            HttpResponseMessage message = _unitOfWork.rideCancellation.Add(rideCancellation, userName, bookingId);
            return message;
        }
    }
}
