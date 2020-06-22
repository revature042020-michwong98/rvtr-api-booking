using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;
using RVTR.Booking.DataContext;

namespace RVTR.Booking.WebApi.Controllers
{
    /// <summary>
    /// The controller layer for the `StayModel.  Provided are HTTP endpoints
    /// for handing calls to the server and determining how to perform
    /// CRUD operations and data to send back to the client.
    /// </summary>
    [ApiController]
    [ApiVersion("0.0")]
    [EnableCors("Public")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StayController : ControllerBase
    {
        private readonly ILogger<StayController> _logger;
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Instanciates the Stay controller with all it's required utilities
        /// </summary>
        /// <param name="logger">Logging utility for displaying details to the console</param>
        /// <param name="unitOfWork">Utility used to ensure transactions are atomic</param>
        public StayController(ILogger<StayController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Deletes Stay resource by id.
        ///
        /// Returns an empty OK response on success
        ///
        /// Otherwise a 404 is returned because the Stay was not found
        /// </summary>
        /// <param name="id">Stay's unique Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.Stay.DeleteAsync(id);
                await _unitOfWork.CommitAsync();

                return Ok();
            }
            catch
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Fetches a list of all Stay records from the databse.
        /// </summary>
        /// <param name="queries">
        /// A POCO used to filter/sort records before they are returned from the response
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromQuery] StaySearchQueries queries = null)
        {
            if (queries == null) return Ok(await _unitOfWork.Stay.SelectAsync());


            return Ok(await _unitOfWork.Stay.SelectAsync(new StaySearchFilter(queries)));
        }

        /// <summary>
        /// Get Stay by id.
        /// </summary>
        /// <param name="id">Id of the Stay record</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _unitOfWork.Stay.SelectAsync(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Create a new Stay record
        /// </summary>
        /// <param name="stay"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(StayModel stay)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Stay.InsertAsync(stay);
                await _unitOfWork.CommitAsync();
                return Accepted(stay);
            }
            else
            {
                // Model state is invalid.
                return BadRequest();
            }

        }

        /// <summary>
        /// Updates a Stay Record
        /// </summary>
        /// <param name="stay"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(StayModel stay)
        {
            _unitOfWork.Stay.Update(stay);
            await _unitOfWork.CommitAsync();

            return Accepted(stay);
        }
    }
}
