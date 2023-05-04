namespace AssetManagementSystemUI
{
    enum Status
    {
        Requested,
        Booked,
        Cancelled,
        Completed,
        Arrived,
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
