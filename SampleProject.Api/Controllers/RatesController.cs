using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Core.Interfaces;
using SampleProject.Models;

namespace SampleProject.Api.Controllers
{
    /// <summary>
    /// Represents rates controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json", "application/xml")]
    [Route("api/rates")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        /// <summary>
        /// The rate calculation action
        /// </summary>
        private IRateCalculationAction rateCalculationAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatesController"/> class.
        /// </summary>
        /// <param name="rateCalculationAction">The rate calculation action.</param>
        /// <exception cref="ArgumentNullException">rateCalculationAction</exception>
        public RatesController(IRateCalculationAction rateCalculationAction)
        {
            this.rateCalculationAction = rateCalculationAction ?? throw new ArgumentNullException(nameof(rateCalculationAction));
        }

        /// <summary>
        /// Gets the specified from date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /2017-03-15T09:10:13.8105498-04:00/2017-03-15T10:10:13.8105498-04:00
        /// 
        /// </remarks>
        /// <returns>A founded rate for specific period</returns>
        /// <response code="200">Returns the founded rate including date range</response>
        /// <response code="400">If passed incorrect parameters</response>
        /// <response code="404">When rate unavailable for given period</response>
        [HttpGet("{fromDate}/{toDate}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RateModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            if ((toDate - fromDate).TotalDays >= 1 || fromDate.Day != toDate.Day || fromDate == toDate)
            {
                return BadRequest("Passed parameters are not satisfy the requirements.");
            }

            var rate = await rateCalculationAction.Calculate(fromDate, toDate);

            if (rate == null)
            {
                return NotFound("Unavailable");
            }

            return Ok(new RateModel { Price = rate.Price, FromDate = fromDate.DateTime, ToDate = toDate.DateTime});
        }
    }
}
