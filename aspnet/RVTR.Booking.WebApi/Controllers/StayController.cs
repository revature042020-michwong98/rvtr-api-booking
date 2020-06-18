using System;
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
  ///
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
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public StayController(ILogger<StayController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Deletes Stay resource by id.
    /// </summary>
    /// <param name="id">Stay's unique Id</param>
    /// <returns>
    ///   Action result stating that the delete was succsessful
    ///   or a NotFound message will display if the
    /// </returns>
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
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] StaySearchQueries queries)
    {
      if (queries == null) return Ok(await _unitOfWork.Stay.SelectAsync());


      return Ok(await _unitOfWork.Stay.SelectAsync(new StaySearchFilter(queries)));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
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
    ///
    /// </summary>
    /// <param name="stay"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(StayModel stay)
    {
      System.Console.WriteLine("Hello");
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
    ///
    /// </summary>
    /// <param name="stay"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put(StayModel stay)
    {
      _unitOfWork.Stay.Update(stay);
      await _unitOfWork.CommitAsync();

      return Accepted(stay);
    }
  }
}
