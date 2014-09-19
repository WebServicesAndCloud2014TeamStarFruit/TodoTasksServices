using System;
using System.Data.Entity;

using TodoTasks.Data;
using TodoTasks.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoTasks.Services.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.JustMock;
using TodoTasks.Data.Repositories;

namespace TodoTasksRestApi.Tests
{
    [TestClass]
    public class CategoryControllerTest
    {
        //[TestMethod]
        //public void GetAll_WhenValid_ShouldReturnCategorysCollection()
        //{
        //    //arrange
        //    FakeRepository<Category> fakeRepo = new FakeRepository<Category>();

        //    var categories = new List<Category>()
        //    {
        //        new Category()
        //        {
        //            Name = "Category 1"
        //        },
        //        new Category()
        //        {
        //            Name = "Category 2"
        //        },
        //        new Category()
        //        {
        //            Name = "Category 3"
        //        }
        //    };

        //    fakeRepo.Entities = categories;

        //    var controller = new CategoriesController(fakeRepo as ITodoTasksData);

        //    //act

        //    var result = controller.All();

        //    //assert

        //    CollectionAssert.AreEquivalent(categories, result.ToList<Category>());
        //}

        //[TestMethod]
        //public void AddCategory_WhenCategoryTextIsValid_ShouldBeAddedToRepoWithLogDateAndStatusPending()
        //{
        //    //arrange
        //    var uow = new FakeTodoTasksData();
        //    uow.IsSaveCalled = false;
        //    var category = new Category()
        //    {
        //        Name = "Valid Category"
        //    };
        //    var controller = new CategoriesController(uow);
        //    this.SetupController(controller);

        //    //act
        //    controller.Create(category);

        //    //assert
        //    Assert.AreEqual(uow.Entities.Count, 1);
        //    var categoryInDb = uow.Entities.First();
        //    Assert.AreEqual(category.Text, categoryInDb.Text);
        //    Assert.IsNotNull(categoryInDb.LogDate);
        //    Assert.AreEqual(Status.Pending, categoryInDb.Status);
        //    Assert.IsTrue(uow.IsSaveCalled);
        //}

        [TestMethod]
        public void GetAll_WhenValid_ShouldReturnCategoriesCollection_WithMocks()
        {
            //arrange
            var uow = Mock.Create<ITodoTasksData>();

            Category[] categories =
            {
                new Category() { Name = "Category1" },
                new Category() { Name = "Category2" }
            };

            Mock.Arrange(() => uow.Categories.All())
                .Returns(() => categories.AsQueryable());

            var controller = new CategoriesController(uow);
            //act
            var result = controller.All();

            //assert
            CollectionAssert.AreEquivalent(categories, result.ToArray<Category>());
        }

        //private void SetupController(ApiController controller)
        //{
        //    string serverUrl = "http://test-url.com";

        //    //Setup the Request object of the controller
        //    var request = new HttpRequestMessage()
        //    {
        //        RequestUri = new Uri(serverUrl)
        //    };
        //    controller.Request = request;

        //    //Setup the configuration of the controller
        //    var config = new HttpConfiguration();
        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional });
        //    controller.Configuration = config;

        //    //Apply the routes of the controller
        //    controller.RequestContext.RouteData =
        //        new HttpRouteData(
        //            route: new HttpRoute(),
        //            values: new HttpRouteValueDictionary
        //            {
        //                { "controller", "categories" }
        //            });
        //}
    }
}
