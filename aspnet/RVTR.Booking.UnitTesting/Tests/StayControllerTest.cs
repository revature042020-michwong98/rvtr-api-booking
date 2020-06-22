using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;
using RVTR.Booking.WebApi.Controllers;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class StayControllerTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<BookingContext> _options = new DbContextOptionsBuilder<BookingContext>().UseSqlite(_connection).Options;
    private readonly StayController _controller;
    private readonly ILogger<StayController> _logger;
    private readonly UnitOfWork _unitOfWork;

    public StayControllerTest()
    {
      var contextMock = new Mock<BookingContext>(_options);
      var loggerMock = new Mock<ILogger<StayController>>();
      var repositoryMock = new Mock<Repository<StayModel>>(new BookingContext(_options));
      var unitOfWorkMock = new Mock<UnitOfWork>(contextMock.Object);

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(1));
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<StayModel>())).Returns(Task.FromResult<StayModel>(null));
      repositoryMock.Setup(m => m.SelectAsync()).Returns(Task.FromResult<IEnumerable<StayModel>>(null));
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).Returns(Task.FromResult<StayModel>(null));
      repositoryMock.Setup(m => m.Update(It.IsAny<StayModel>()));
      unitOfWorkMock.Setup(m => m.Stay).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new StayController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(0);
      var resultPass = await _controller.Delete(1);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(0);
      var resultOne = await _controller.Get(1);
      var resultQuery = await _controller.Get(new StaySearchQueries());

      Assert.IsType<OkObjectResult>(resultMany);
      Assert.IsType<NotFoundObjectResult>(resultFail);
      Assert.IsType<OkObjectResult>(resultOne);
      Assert.IsType<OkObjectResult>(resultQuery);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new StayModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Post_Controller_Post_Invalid()
    {
      _controller.ModelState.AddModelError("Post", "InvalidModel");
      var result = await _controller.Post(new StayModel());

      Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.Put(new StayModel());

      Assert.NotNull(resultPass);
    }
  }
}
