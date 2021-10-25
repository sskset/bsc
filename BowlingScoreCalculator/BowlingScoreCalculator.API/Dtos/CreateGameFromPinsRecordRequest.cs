using FluentValidation;

namespace BowlingScoreCalculator.API.Dtos
{
    public class CreateGameFromPinsRecordRequest
    {
        public int[] PinsDowned { get; set; }
    }

    public class CreateGameFromPinsRecordRequestValidator : AbstractValidator<CreateGameFromPinsRecordRequest>
    {
        public CreateGameFromPinsRecordRequestValidator()
        {
            RuleFor(x => x.PinsDowned)
                .NotNull()
                .NotEmpty();
        }
    }
}
