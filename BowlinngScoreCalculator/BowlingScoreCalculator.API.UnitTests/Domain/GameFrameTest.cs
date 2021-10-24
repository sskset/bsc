using BowlingScoreCalculator.API.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BowlingScoreCalculator.API.UnitTests.Domain
{
    public class GameFrameTest
    {
        [Fact]
        public void Give_incorrect_pins_down_Should_throw_ex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameFrame(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameFrame(11));
        }

        [Fact]
        public void Give_correct_pinsdown_Should_create_instance()
        {
            var frame1 = new GameFrame(10);
            Assert.NotNull(frame1);
            Assert.False(frame1.IsFinalFrame);

            var frame2 = new GameFrame(5, true);
            Assert.NotNull(frame2);
            Assert.True(frame2.IsFinalFrame);
        }

        // TODO: ADD MORE UNIT TESTS
    }
}
