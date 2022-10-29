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
            DateOfBirth = new DateTime(2001,10,18)
        };

        var result = _rookiesController.Create(mockModel);

        Assert.IsInstanceOf<RedirectToActionResult>(result);

        Assert.AreEqual((result as RedirectToActionResult).ActionName,"Index");
    }
}