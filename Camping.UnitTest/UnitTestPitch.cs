using Camping.Entities.DataClasses;

namespace Camping.UnitTest
{
    public class UnitTestPitch
    {
        [Fact]
        public void Pitch_New_AssertsTrue()
        {
            _ = new Pitch(0, "Valid");

            Assert.True(true);
        }

        [Fact]
        public void Pitch_Mutation_AssertsTrue()
        {
            var p = new Pitch(0, "Valid");

            p.PitchName = "x";

            Assert.True(true);
        }

        [Fact]
        public void Pitch_Mutation_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var p = new Pitch(0, "Valid");

                p.PitchName = "";
            });
        }
    }
}
