using Xunit;
using Generators;

namespace Theatre.Tests
{

    public class BaseAlphaGeneratorTests
    {
        [Fact]
        public void CustomAlpha_CheckOutput()
        {
            // Arrange
            var g = new BaseAlphaGenerator("xyz3456789");

            // Act
            int n = 1042;
            for (int i = 0; i < n; i++)
            {
                g.Next();
            }

            // Assert
            Assert.Equal("yx4z", g.Next());
        }
    }


    public class NumericalGeneratorTests
    {
        [Fact]
        public void CheckOutput()
        {
            // Arrange
            var g = new NumericalGenerator();

            // Act
            int n = 1042;
            for (int i = 0; i < n; i++)
            {
                g.Next();
            }

            // Assert
            Assert.Equal(n.ToString(), g.Next());
        }
    }


    public class AlphabeticalGeneratorTests
    {
        [Fact]
        public void CheckOutput()
        {
            // Arrange
            AlphabeticalGenerator g = new AlphabeticalGenerator();

            // Act
            int n = 1042;
            for (int i = 0; i < n; i++)
            {
                g.Next();
            }

            // Assert
            Assert.Equal("BOC", g.Next());
        }
    }
}
