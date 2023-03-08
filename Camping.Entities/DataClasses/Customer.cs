using Camping.Entities.Interfaces;

namespace Camping.Entities.DataClasses
{
    /// <summary>
    /// Represents a Customer from the database
    /// </summary>
    public struct Customer : IDataClass
    {
        private int _customerId;
        private string _fullName = string.Empty;
        private string _emailAddress = string.Empty;
        private string _phoneNumber = string.Empty;

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

        public string FullName 
        { 
            get => _fullName; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Navn kan ikke være Null, Empty eller Whitespace.", nameof(value));
                else if (_fullName != value)
                    _fullName = value;
            }
        }

        public string EmailAddress 
        { 
            get => _emailAddress; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email kan ikke være Null, Empty eller Whitespace.", nameof(value));
                else if (_emailAddress != value)
                    _emailAddress = value;
            }
        }

        public string PhoneNumber 
        { 
            get => _phoneNumber; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Telefon nummer kan ikke være Null, Empty eller Whitespace.", nameof(value));
                else if (_phoneNumber != value)
                    _phoneNumber = value;
            }
        }

        public Customer(string fullName, string emailAddress, string phoneNumber) 
            : this(0, fullName, emailAddress, phoneNumber) { }

        public Customer(int customerId, string fullName, string emailAddress, string phoneNumber)
        {
            CustomerId = customerId;
            FullName = fullName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }
    }
}
