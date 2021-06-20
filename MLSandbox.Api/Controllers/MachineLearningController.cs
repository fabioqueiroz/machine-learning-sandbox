using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MLSandbox.Api.Models;
using MLSandbox.Api.Utilities;
using MLSandboxML.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLSandbox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineLearningController : ControllerBase
    {
        private readonly ILogger<MachineLearningController> _logger;
        public MachineLearningController(ILogger<MachineLearningController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AppHealth()
        {
            return Ok("Application working");
        }

        [HttpPost]
        [EnableCors("LocalhostPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddComment([FromBody] string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                return BadRequest("No text in the body");
            }
            // Add input data
            var input = new ModelInput() { Comment = comment };

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);

            var sentiment = result.Prediction == "1" ? nameof(Predictions.Positive) : nameof(Predictions.Negative);

            return Ok(JsonConvert.SerializeObject(new { prediction = sentiment }));
        }
    }
}
