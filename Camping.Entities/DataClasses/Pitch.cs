using Camping.Entities.Interfaces;

namespace Camping.Entities.DataClasses
{
    public struct Pitch : IDataClass
    {
        private int _pitchId;
        private string _pitchName = string.Empty;

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

        public string PitchName 
        { 
            get => _pitchName; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Navn kan ikke være Null, Empty eller Whitespace.", nameof(value));
                else if (_pitchName != value)
                    _pitchName = value;
            }
        }

        public Pitch(string pitchName)
            : this(0, pitchName) { }

        public Pitch(int pitchId, string pitchName)
        {
            PitchId = pitchId;
            PitchName = pitchName;
        }
    }
}
