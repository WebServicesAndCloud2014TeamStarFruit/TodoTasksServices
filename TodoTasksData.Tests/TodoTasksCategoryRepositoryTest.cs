namespace TodoTasksData.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Transactions;
    using TodoTasks.Data;
    using TodoTasks.Models;

    [TestClass]
    public class TodoTasksCategoryRepositoryTest
    {
        static TransactionScope tran;

        [TestInitialize]
        public void TestInit()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void All_ShouldReturnAllObjects()
        {
            //arrange
            var dbContext = new TodoTasksDbContext();
            var repo = new TodoTasks.Data.TodoTasksData();

            //act
            var actual = repo.Categories.All().Select(c => c.Name).ToList();
            var expected = dbContext.Categories.Select(c => c.Name).ToList();
            //assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void Add_WhenObjectAdded_ShouldReturnAddedobjects()
        {
            //arrange
            var dbContext = new TodoTasksDbContext();
            var repo = new TodoTasks.Data.TodoTasksData();

            Category category = this.GetValidTestCategory();
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            repo.Categories.Add(category);

            //act
            var actual = repo.Categories.All().FirstOrDefault(c => c.Id == category.Id);
            var expected = dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        public void Delete_WhenObjectDeleted_ShouldNotReturnObject()
        {
            //arrange
            var dbContext = new TodoTasksDbContext();
            var repo = new TodoTasks.Data.TodoTasksData();

            Category category = dbContext.Categories.FirstOrDefault();
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            repo.Categories.Delete(category);

            //act
            var actual = repo.Categories.All().FirstOrDefault(c => c.Id == category.Id);
            var expected = dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Find_WhenObjectIsInDb_ShouldReturnObject()
        {
            //arrange
            var category = this.GetValidTestCategory();

            var dbContext = new TodoTasksDbContext();
            var repo = new TodoTasksData();

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            //act
            var categoryInDb = repo.Categories.Find(category.Id);

            //asesrt

            Assert.IsNotNull(categoryInDb);
            Assert.AreEqual(category.Name, categoryInDb.Name);
        }


        private Category GetValidTestCategory()
        {
            var ran = new Random();
            
            var category = new Category()
            {
                Name = "Test category " + ran.Next(0, 100)
            };
            return category;
        }

    }
}
