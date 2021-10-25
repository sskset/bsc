using BowlingScoreCalculator.API.Domain;
using BowlingScoreCalculator.API.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BowlingScoreCalculator.API.UnitTests.Domain
{
    public class BowlingGameTest
    {
        [Fact]
        public void Shoul_create_game_without_arguments()
        {
            var game = new BowlingGame();
            Assert.NotNull(game);
        }

        [Fact]
        public void Given_null_input_When_call_create_Should_throw_argument_exception()
        {
            Assert.Throws<ArgumentException>(
                () => BowlingGame.Create(null)
            );
        }

        [Fact]
        public void Given_1_throw_Should_indicate_game_uncompleted()
        {
            var game = new BowlingGame();
            game.Throw(1);

            Assert.False(game.GameCompleted);
        }

        [Fact]
        public void Given_all_strikes_Should_calculate_full_scores()
        {
            //Example 1 – Perfect Game
            var game = BowlingGame.Create(10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);
            var score = game.ComputeScores();

            Assert.Equal(300, score);
            Assert.True(game.GameCompleted);
        }

        [Fact]
        public void Given_Out_of_boundary_inputs_Should_throw_exception()
        {
            Assert.Throws<BowlingGameException>(() => BowlingGame.Create(10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10));
        }

        [Fact]
        public void Given_all_1_pinsdown_Should_return_expected()
        {
            // Example 2 – 6 frames completed, all throws 1
            var game = BowlingGame.Create(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            game.ComputeScores();
            Assert.Equal(new string[] { "2", "4", "6", "8", "10", "12" }, game.ProgressFrameScores.ToArray());
        }

        [Fact]
        public void Given_spare_spikes_inputs_Should_calculate_correct()
        {
            // Example 3 – 7 frames completed, spare and strikes example
            var game = BowlingGame.Create(1, 1, 1, 1, 9, 1, 2, 8, 9, 1, 10, 10);
            game.ComputeScores();

            Assert.Equal(new string[] { "2", "4", "16", "35", "55", "*", "*" }, game.ProgressFrameScores.ToArray());
        }

        // TODO: MORE TEST COVERAGE
    }
}
