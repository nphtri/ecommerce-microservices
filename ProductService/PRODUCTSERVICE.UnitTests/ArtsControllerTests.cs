using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PRODUCTSERVICE.API.Controllers;
using PRODUCTSERVICE.API.Dtos;
using PRODUCTSERVICE.API.Services;

namespace PRODUCTSERVICE.UnitTests
{
  [TestClass]
  public class ArtsControllerTests
  {
    private readonly Mock<IArtService> artServiceStub = new Mock<IArtService>();
    private readonly Mock<IArtistService> artistServiceStub = new Mock<IArtistService>();
    private readonly Random random = new Random();
    [TestMethod]
    public void GetArtById_WithUnexisting_ReturnNotFound()
    {
      // arrange
      artServiceStub.Setup(service => service.GetArtById(It.IsAny<int>())).Returns((ArtReadDto)null);
      var controller = new ArtsController(artServiceStub.Object, artistServiceStub.Object);
      // act
      var result = controller.GetArtById(random.Next());
      //assert
      result.Result.Should().BeOfType<NotFoundResult>();
    }

    [TestMethod]
    public void GetArtById_WithExisting_ReturnExpectedArt()
    {
      // arrange
      var expected = CreateRandomArt();
      artServiceStub.Setup(service => service.GetArtById(It.IsAny<int>())).Returns(expected);
      var controller = new ArtsController(artServiceStub.Object, artistServiceStub.Object);
      // act
      var result = controller.GetArtById(random.Next());
      //assert
      result.Result.Should().BeOfType<OkObjectResult>();
      var ok = (OkObjectResult)result.Result;
      ok.Value.Should().BeEquivalentTo(expected, options => options.ComparingByMembers<ArtReadDto>());
    }

    private ArtReadDto CreateRandomArt()
    {
      return new()
      {
        Id = random.Next(),
        Description = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        Image = Guid.NewGuid().ToString(),
        Short = Guid.NewGuid().ToString(),
        Style = Guid.NewGuid().ToString()
      };
    }
  }
}
