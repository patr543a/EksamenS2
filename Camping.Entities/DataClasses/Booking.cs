using Camping.Entities.Interfaces;

namespace Camping.Entities.DataClasses
{
    public struct Booking : IDataClass
    {
        private int _bookingId;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _adults;
        private int _children;
        private int _pitchId;
        private int _customerId;

        public int BookingId 
        { 
            get => _bookingId; 
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Id kan ikke være mindre end nul");
                else if (_bookingId != value)
                    _bookingId = value;
            }
        }

        public DateTime StartDate 
        { 
            get => _startDate; 
            set
            {
                if (value < DateTime.UtcNow.Subtract(new TimeSpan(1000, 0, 0, 0)) || value > DateTime.UtcNow.Add(new TimeSpan(1000, 0, 0, 0)))
                    throw new ArgumentOutOfRangeException(nameof(value), "Start dato kan ikke være mere end 1000 dage siden eller om 1000 dage");
                else if (_startDate != value)
                    _startDate = value;
            }
        }
        public DateTime EndDate 
        {
            get => _endDate;
            set
            {
                if (_startDate > value)
                    throw new ArgumentOutOfRangeException(nameof(value), "Slut dato kan ikke være før start dato");
                else if (value < DateTime.UtcNow.Subtract(new TimeSpan(1000, 0, 0, 0)) || value > DateTime.UtcNow.Add(new TimeSpan(1000, 0, 0, 0)))
                    throw new ArgumentOutOfRangeException(nameof(value), "Slut dato kan ikke være mere end 1000 dage siden eller om 1000 dage");
                else if (_endDate != value)
                    _endDate = value;
            }
        }

        public int Adults 
        { 
            get => _adults; 
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Der kan ikke være mindre end en voksen");
                else if (_adults != value)
                    _adults = value;
            }
        }

        public int Children 
        { 
            get => _children; 
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Der kan ikke være mindre end nul børn");
                else if (_children != value)
                    _children = value;
            }
        }
        
        public int PitchId 
        { 
            get => _pitchId; 
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Id kan ikke være mindre end nul");
                else if (_pitchId != value)
                    _pitchId = value;
            }
        }

        public int CustomerId 
        { 
            get => _customerId; 
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Id kan ikke være mindre end nul");
                else if (_customerId != value)
                    _customerId = value;
            } 
        }

        public Booking(DateTime startDate, DateTime endDate, int adults, int children, int pitchId, int customerId)
            : this(0, startDate, endDate, adults, children, pitchId, customerId) { }

        public Booking(int bookingId, DateTime startDate, DateTime endDate, int adults, int children, int pitchId, int customerId)
        {
            BookingId = bookingId;
            StartDate = startDate;
            EndDate = endDate;
            Adults = adults;
            Children = children;
            PitchId = pitchId;
            CustomerId = customerId;
        }
    }
}
