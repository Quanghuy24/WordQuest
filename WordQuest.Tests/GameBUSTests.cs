using Xunit;
using WordQuest.BUS;

namespace WordQuest.Tests
{
    public class GameBUSTests
    {
        [Theory]
        [InlineData(10, 5, 8, 10, 3)] // correctCount >= t3 (10) -> 3 stars
        [InlineData(9, 5, 8, 10, 2)]  // correctCount >= t2 (8) -> 2 stars
        [InlineData(8, 5, 8, 10, 2)]  // correctCount == t2 (8) -> 2 stars
        [InlineData(7, 5, 8, 10, 1)]  // correctCount >= t1 (5) -> 1 star
        [InlineData(5, 5, 8, 10, 1)]  // correctCount == t1 (5) -> 1 star
        [InlineData(4, 5, 8, 10, 0)]  // correctCount < t1 (5) -> 0 stars
        [InlineData(0, 5, 8, 10, 0)]  // correctCount < t1 (5) -> 0 stars
        public void ComputeStars_ReturnsCorrectStarCount(int correctCount, int t1, int t2, int t3, int expectedStars)
        {
            // Act
            int stars = GameBUS.ComputeStars(correctCount, t1, t2, t3);

            // Assert
            Assert.Equal(expectedStars, stars);
        }
    }
}

