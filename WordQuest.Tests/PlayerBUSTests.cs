using System;
using Xunit;
using WordQuest.DAL;

namespace WordQuest.Tests
{
    public class PlayerBUSTests
    {
        [Fact]
        public void CalculateNewStreak_FirstTime_Returns1AndTrue()
        {
            DateTime today = new DateTime(2026, 5, 30);
            var result = PlayerDAL.CalculateNewStreak(null, 0, today);
            
            Assert.Equal(1, result.newStreak);
            Assert.True(result.isNewDay);
        }

        [Fact]
        public void CalculateNewStreak_SameDay_ReturnsSameStreakAndFalse()
        {
            DateTime today = new DateTime(2026, 5, 30);
            DateTime lastPlayed = new DateTime(2026, 5, 30);
            int currentStreak = 5;

            var result = PlayerDAL.CalculateNewStreak(lastPlayed, currentStreak, today);
            
            Assert.Equal(5, result.newStreak);
            Assert.False(result.isNewDay);
        }

        [Fact]
        public void CalculateNewStreak_NextDay_IncrementsStreakAndReturnsTrue()
        {
            DateTime today = new DateTime(2026, 5, 30);
            DateTime lastPlayed = new DateTime(2026, 5, 29);
            int currentStreak = 5;

            var result = PlayerDAL.CalculateNewStreak(lastPlayed, currentStreak, today);
            
            Assert.Equal(6, result.newStreak);
            Assert.True(result.isNewDay);
        }

        [Fact]
        public void CalculateNewStreak_MissedDay_ResetsTo1AndReturnsTrue()
        {
            DateTime today = new DateTime(2026, 5, 30);
            DateTime lastPlayed = new DateTime(2026, 5, 28);
            int currentStreak = 5;

            var result = PlayerDAL.CalculateNewStreak(lastPlayed, currentStreak, today);
            
            Assert.Equal(1, result.newStreak);
            Assert.True(result.isNewDay);
        }
    }
}

