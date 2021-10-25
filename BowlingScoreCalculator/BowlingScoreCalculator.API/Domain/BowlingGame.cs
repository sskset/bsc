using BowlingScoreCalculator.API.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoreCalculator.API.Domain
{
    public class BowlingGame
    {
        private readonly List<GameFrame> _gameFrames;

        public BowlingGame()
        {
            _gameFrames = new List<GameFrame>(10);
        }

        public int FrameCount => _gameFrames.Count;

        public bool GameCompleted
        {
            get
            {
                return _gameFrames.Count == 10 && _gameFrames.All(x => x.IsClosed) && _gameFrames.All(x=>x.IsScoreComputed);
            }
        }

        public IEnumerable<int> FrameScores
        {
            get
            {
                return _gameFrames.Select(x => x.FrameScore);
            }
        }

        public IEnumerable<string> ProgressFrameScores
        {
            get
            {
                int score = 0;

                for (int i = 0; i < _gameFrames.Count; i++)
                {
                    if (_gameFrames[i].IsClosed)
                    {
                        if (_gameFrames[i].IsScoreComputed)
                        {
                            score += _gameFrames[i].FrameScore;
                            yield return score.ToString();
                        }
                        else
                        {
                            yield return "*";
                        }
                    }
                    else
                    {
                        yield return "*";
                    }
                }
            }
        }


        public int ComputeScores()
        {
            for (int i = 0; i < FrameCount; i++)
            {
                var currentFrame = _gameFrames[i];
                if (currentFrame != null)
                {
                    var nextIndex = i + 1;
                    var afterNextIndex = nextIndex + 1;

                    currentFrame.ComputeScore(
                        _gameFrames.ElementAtOrDefault(nextIndex),
                        _gameFrames.ElementAtOrDefault(afterNextIndex));
                }
            }

            return _gameFrames.Sum(frame => frame.FrameScore);
        }

        public void Throw(int pinsDown)
        {
            if (pinsDown < 0 || pinsDown > 10)
            {
                throw new ArgumentOutOfRangeException($"{nameof(pinsDown)} should be between 0 and 10.");
            }

            var currentFrame = _gameFrames.LastOrDefault();
            if (currentFrame == null)
            {
                _gameFrames.Add(new GameFrame(pinsDown));
            }
            else
            {
                if (currentFrame.IsClosed)
                {
                    if (FrameCount < 9)
                    {
                        _gameFrames.Add(new GameFrame(pinsDown));
                    }
                    else if (FrameCount == 9)
                    {
                        _gameFrames.Add(new GameFrame(pinsDown, true));
                    }
                    else
                    {
                        throw new BowlingGameException("The game is closed.");
                    }
                }
                else
                {
                    currentFrame.TryAdd(pinsDown);
                }
            }
        }

        /// <summary>
        /// Provide a static method to create instance
        /// </summary>
        /// <param name="pinsDownRecords"></param>
        /// <returns></returns>
        public static BowlingGame Create(params int[] pinsDownRecords)
        {
            if (pinsDownRecords == null || !pinsDownRecords.Any())
            {
                throw new ArgumentException($"{nameof(pinsDownRecords)} is null or empty.");
            }

            var game = new BowlingGame();

            foreach (var @record in pinsDownRecords)
            {
                game.Throw(@record);
            }

            return game;
        }
    }
}
