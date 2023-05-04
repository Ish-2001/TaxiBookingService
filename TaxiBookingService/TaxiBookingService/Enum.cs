namespace TaxiBookingService
{
    enum Status
    {
        Requested,
        Booked,
        Cancelled,
        Completed,
        Started
    }
    enum Role
    {
        User,
        Driver,
        Admin
    }

    enum PaymentModes
    {
        Cash,
        Wallet,
        UPI
    }
}
