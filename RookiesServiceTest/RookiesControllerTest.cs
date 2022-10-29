using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCAssigmentDay2.Controllers;
using MVCAssigmentDay2.Models;
using MVCAssigmentDay2.Services;
using NUnit.Framework;
namespace RookiesControllerTest;

public class RookiesControllerTest
{
    private RookiesController _rookiesController;
    private Mock<IPersonServices> _personService;
    [SetUp]
    public void Setup()
    {
        _personService = new Mock<IPersonServices>();
        _rookiesController = new RookiesController(_personService.Object);
    }

    [Test]
    public void AddPerson_Success()
    {
        var mockModel = new PersonCreateModel
        {
            FirstName = "Nam",
            LastName = "Phan",
            Gender = "Male",
            DateOfBirth = new DateTime(2001, 10, 18)
        };

        var result = _rookiesController.Create(mockModel);

        Assert.IsInstanceOf<RedirectToActionResult>(result);

        Assert.AreEqual((result as RedirectToActionResult).ActionName, "Index");
    }

    [Test]
    public void Details_ReturnsToAction_InValidIndex()
    {
        _personService.Setup(p => p.GetOne(It.IsAny<int>())).Returns(null as PersonModel);
        var index = 0;

        var result = _rookiesController.Detail(index);

        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.That("Index", Is.EqualTo((result as RedirectToActionResult).ActionName));
    }

    [Test]
    public void Details_ReturnsToAction_ValidIndex()
    {
        var expectModel = new PersonModel
        {
            FirstName = "Nam",
            LastName = "Phan"
        };
        _personService.Setup(p => p.GetOne(It.IsAny<int>())).Returns(expectModel);
        var index = 0;

        var result = _rookiesController.Detail(index) as ViewResult;

        Assert.IsNotNull(result);

        var returnModel = result.Model as PersonModel;

        Assert.AreEqual(expectModel.FirstName, returnModel.FirstName);

        Assert.AreEqual(expectModel.LastName, returnModel.LastName);
    }

}