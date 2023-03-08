using Camping.Entities.DataClasses;

namespace Camping.Entities.Classes
{
    /// <summary>
    /// Converts bookings, customers and pitches into a list of BookingInfo
    /// </summary>
    public struct BookingWrapper
    {
        public List<BookingInfo> Bookings { get; set; } = new();

        public BookingWrapper(List<Booking> bookings, List<Customer> customers, List<Pitch> pitches)
        {
            // Loop over bookings
            foreach (var booking in bookings)
            {
                // Get customer and pitch
                var customer = customers.Where(c => c.CustomerId == booking.CustomerId).FirstOrDefault();
                var pitch = pitches.Where(p => p.PitchId == booking.PitchId).FirstOrDefault();

                // Add new BookingInfo to the list
                Bookings.Add(new() 
                {
                    CustomerName = customer.FullName,
                    CustomerEmail = customer.EmailAddress,
                    CustomerPhone = customer.PhoneNumber,
                    PitchName = pitch.PitchName,
                    Adults = booking.Adults,
                    Children = booking.Children,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                });
            }
        }
    }

    /// <summary>
    /// Summary of Booking and it's references to a customer and a pitch
    /// </summary>
    /// <param name="CustomerName">Customer name</param>
    /// <param name="CustomerEmail">Customer email</param>
    /// <param name="CustomerPhone">Customer phone number</param>
    /// <param name="Adults">Adult count</param>
    /// <param name="Children">Children count</param>
    /// <param name="StartDate">Starting date</param>
    /// <param name="EndDate">Ending date</param>
    public record struct BookingInfo(string CustomerName, string CustomerEmail, string? CustomerPhone, string PitchName, int Adults, int Children, DateTime StartDate, DateTime EndDate);
}
