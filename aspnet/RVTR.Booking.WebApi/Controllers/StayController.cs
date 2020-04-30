using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.WebApi.Controllers
{
  [ApiController]
  [EnableCors()]
  [Route("api/[controller]")]
  public class StayController : ControllerBase
  {
    private readonly ILogger<StayController> _logger;
    private readonly UnitOfWork _unitOfWork;

    public StayController(ILogger<StayController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    [HttpDelete("{id}")]
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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Booking.SelectAsync());
    }

    [HttpGet("{id")]
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

    [HttpPost]
    public async Task<IActionResult> Post(StayModel stay)
    {
      await _unitOfWork.Stay.InsertAsync(stay);
      await _unitOfWork.CommitAsync();

      return Accepted(stay);
    }

    [HttpPut]
    public async Task<IActionResult> Put(StayModel stay)
    {
      _unitOfWork.Stay.Update(stay);
      await _unitOfWork.CommitAsync();

      return Accepted(stay);
    }
  }
}
