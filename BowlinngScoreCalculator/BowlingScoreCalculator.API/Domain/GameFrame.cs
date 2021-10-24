using BowlingScoreCalculator.API.Domain.Exceptions;
using System;

namespace BowlingScoreCalculator.API.Domain
{
    public class GameFrame
    {
        public bool IsFinalFrame { get; }
        public bool IsScoreComputed { get; private set; }
        public bool IsClosed
        {
            get
            {
                if (IsFinalFrame)
                {
                    if (IsFirstThrowStrike)
                    {
                        if (IsSecondThrowStrike)
                        {
                            return ThirdThrow.HasValue;
                        }
                        else
                        {
                            return SecondThrow.HasValue && ThirdThrow.HasValue;
                        }
                    }
                    else
                    {
                        return SecondThrow.HasValue;
                    }
                }
                else
                {
                    if (IsFirstThrowStrike)
                    {
                        return true;
                    }
                    else
                    {
                        return SecondThrow.HasValue;
                    }
                }
            }
        }
        public int FirstThrow { get; private set; }
        public int? SecondThrow { get; private set; }
        public int? ThirdThrow { get; private set; }
        /// <summary>
        /// This reprents the score accuqired in this frame.
        /// It's not the game score.
        /// </summary>
        public int FrameScore { get; private set; }
        public bool IsFirstThrowStrike => FirstThrow == 10;
        public bool IsSecondThrowStrike => SecondThrow == 10;
        public int ThrowCount
        {
            get
            {
                if (IsFinalFrame)
                {
                    return SecondThrow.HasValue && ThirdThrow.HasValue ? 3 : SecondThrow.HasValue ? 2 : 1;
                }

                return SecondThrow.HasValue ? 2 : 1;
            }
        }

        public bool IsStripe
        {
            get
            {
                if (!IsFirstThrowStrike)
                {
                    return (this.FirstThrow + this.SecondThrow ?? 0) == 10;
                }

                return false;
            }
        }

        public GameFrame(int firstThrow, bool isFinalFrame = false)
        {
            if (firstThrow < 0 || firstThrow > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(firstThrow));
            }
            FirstThrow = firstThrow;
            IsFinalFrame = isFinalFrame;
        }

        public void TryAdd(int pinsDown)
        {
            if (pinsDown < 0 || pinsDown > 10)
            {
                throw new ArgumentException("Invalid input.");
            }

            if (IsClosed)
            {
                throw new BowlingGameException("This frame is closed.");
            }

            if (IsFinalFrame)
            {
                if (IsFirstThrowStrike)
                {
                    if (SecondThrow.HasValue)
                    {
                        if (ThirdThrow.HasValue)
                        {
                            throw new BowlingGameException("This frame is closed.");
                        }

                        if (IsSecondThrowStrike)
                        {
                            ThirdThrow = pinsDown;
                        }
                        else
                        {
                            if (SecondThrow.Value + pinsDown > 10)
                            {
                                throw new BowlingGameException("Cannot add invalid value.");
                            }

                            ThirdThrow = pinsDown;
                        }
                    }
                    else
                    {
                        SecondThrow = pinsDown;
                    }
                }
                else
                {
                    SecondThrow = pinsDown;
                }
            }
            else
            {
                if (SecondThrow.HasValue)
                {
                    throw new BowlingGameException("This frame is closed.");
                }
                else
                {
                    if (FirstThrow + pinsDown > 10)
                    {
                        throw new BowlingGameException("Cannot add invalid value.");
                    }

                    SecondThrow = pinsDown;
                }
            }
        }

        public virtual void ComputeScore(GameFrame nextFrame, GameFrame afterNextFrame)
        {
            if (IsFinalFrame)
            {
                if (IsFirstThrowStrike)
                {
                    if (IsSecondThrowStrike)
                    {
                        if (ThirdThrow.HasValue)
                        {
                            IsScoreComputed = true;
                        }

                        this.FrameScore = (ThirdThrow ?? 0) + 20;
                    }
                    else
                    {
                        IsScoreComputed = SecondThrow.HasValue && ThirdThrow.HasValue;
                        this.FrameScore = 10 + (SecondThrow ?? 0) + (ThirdThrow ?? 0);
                    }
                }
                else
                {
                    IsScoreComputed = SecondThrow.HasValue;
                    this.FrameScore = FirstThrow + (SecondThrow ?? 0);
                }
            }
            else
            {
                if (IsFirstThrowStrike)
                {
                    if (nextFrame?.IsFinalFrame == true)
                    {
                        if (nextFrame?.IsFirstThrowStrike == true)
                        {
                            this.FrameScore = 20 + (nextFrame?.SecondThrow ?? 0);
                            IsScoreComputed = nextFrame?.SecondThrow.HasValue == true;
                        }
                        else
                        {
                            this.FrameScore = 10 + (nextFrame?.FirstThrow ?? 0) + (nextFrame?.SecondThrow ?? 0);
                            IsScoreComputed = nextFrame?.SecondThrow.HasValue == true;
                        }
                    }
                    else
                    {
                        if (nextFrame?.IsFirstThrowStrike == true)
                        {
                            this.FrameScore = 20 + (afterNextFrame?.FirstThrow ?? 0);
                            IsScoreComputed = afterNextFrame != null;
                        }
                        else
                        {
                            this.FrameScore = 10 + (nextFrame?.FirstThrow ?? 0) + (nextFrame?.SecondThrow ?? 0);
                            IsScoreComputed = nextFrame?.SecondThrow.HasValue == true;
                        }
                    }
                }
                else if (IsStripe)
                {
                    this.FrameScore = 10 + (nextFrame?.FirstThrow ?? 0);
                    IsScoreComputed = nextFrame != null;
                }
                else
                {
                    this.FrameScore = FirstThrow + (SecondThrow ?? 0);
                    IsScoreComputed = SecondThrow.HasValue;
                }
            }
        }
    }
}
