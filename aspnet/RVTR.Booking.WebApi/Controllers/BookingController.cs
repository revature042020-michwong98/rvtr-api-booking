using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;
using RVTR.Booking.DataContext;

namespace RVTR.Booking.WebApi.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [ApiVersion("0.0")]
    [EnableCors("Public")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="unitOfWork"></param>
        public BookingController(ILogger<BookingController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Deletes booking by id.
        /// </summary>
        /// <param name="id">Id of booking.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.Booking.DeleteAsync(id);
                await _unitOfWork.CommitAsync();

                return Ok();
            }
            catch
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Get all bookings.
        /// </summary>
        /// <param name="bookingSearchQueries">Search parameters from the query string to filter bookings.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromQuery] BookingSearchQueries bookingSearchQueries)
        {
            // No booking search queries.
            if (bookingSearchQueries == null)
                // Get all bookings.
                return Ok(await _unitOfWork.Booking.SelectAsync());

            // Get all bookings using booking search queries.
            return Ok(await _unitOfWork.Booking.SelectAsync(new BookingSearchFilter(bookingSearchQueries)));
        }

        /// <summary>
        /// Get booking by id.
        /// </summary>
        /// <param name="id">Id of booking.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _unitOfWork.Booking.SelectAsync(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Create a new booking.
        /// </summary>
        /// <param name="booking">Object containing booking information.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(BookingModel booking)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Booking.InsertAsync(booking);
                await _unitOfWork.CommitAsync();

                return CreatedAtAction(nameof(Post), booking);
            }
            else
            {
                // Model state is invalid.
                return BadRequest();
            }
        }

        /// <summary>
        /// Update booking information.
        /// </summary>
        /// <param name="booking">Object containing booking information.</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(BookingModel booking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Booking.Update(booking);
                    await _unitOfWork.CommitAsync();

                    return Ok(booking);
                }
                catch (DbUpdateException)
                {
                    // Entity Framework Core update exception.
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
