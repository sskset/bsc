using BowlingScoreCalculator.API.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BowlingScoreCalculator.API.UnitTests.Controllers
{
    public class ScoresControllerTest
    {

        [Fact]
        public void Given_null_logger_Should_throw_argument_null_ex()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoresController(null));
        }

        // TODO: MORE TEST COVERAGE
        // SORRY PRETTY BUSY THESE DAYS NO TIME TO FINISH ALL UNIT TESTS
    }
}
