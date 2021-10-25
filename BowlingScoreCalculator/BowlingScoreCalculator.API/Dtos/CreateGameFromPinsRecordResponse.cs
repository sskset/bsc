using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScoreCalculator.API.Dtos
{
    public class CreateGameFromPinsRecordResponse
    {
        public IEnumerable<string> ProgressFrameScores { get; set; } = Enumerable.Empty<string>();
        public bool GameCompleted { get; set; } = false;
    }
}
