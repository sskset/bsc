using BowlingScoreCalculator.API.Domain;
using BowlingScoreCalculator.API.Domain.Exceptions;
using BowlingScoreCalculator.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BowlingScoreCalculator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly ILogger<ScoresController> _logger;
        public ScoresController(ILogger<ScoresController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateGameFromPinsRecordResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(Exception))]
        public IActionResult GetFrameScoresFromPinsDown(CreateGameFromPinsRecordRequest payload)
        {
            // FLUENT VALIDATION INTERGRATION WOULD VALIDATE REQUEST
            // SO HRERE WE NO NEED TO VALIDATE IT'S NULL OR NOT

            try
            {
                var game = BowlingGame.Create(payload.PinsDowned);
                game.ComputeScores();

                var response = new CreateGameFromPinsRecordResponse
                {
                    GameCompleted = game.GameCompleted,
                    ProgressFrameScores = game.ProgressFrameScores
                };

                return Ok(response);
            }
            catch(BowlingGameException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid input");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred.");
            }
        }
    }
}