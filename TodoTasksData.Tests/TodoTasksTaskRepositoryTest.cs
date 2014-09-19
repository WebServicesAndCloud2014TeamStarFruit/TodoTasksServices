namespace TodoTasksData.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Transactions;
    using TodoTasks.Data;
    using TodoTasks.Models;

    [TestClass]
    public class TodoTasksTaskRepositoryTest
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
            var actual = repo.Tasks.All().Select(t => t.Content).ToList();
            var expected = dbContext.Tasks.Select(t => t.Content).ToList();
            //assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void Add_WhenObjectAdded_ShouldReturnAddedobjects()
        {
            //arrange
            var dbContext = new TodoTasksDbContext();
            var repo = new TodoTasks.Data.TodoTasksData();

            TodoTask task = this.GetValidTestTask();
            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();
            repo.Tasks.Add(task);

            //act
            var actual = repo.Tasks.All().FirstOrDefault(t => t.Id == task.Id);
            var expected = dbContext.Tasks.FirstOrDefault(t => t.Id == task.Id);
            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Content, actual.Content);
            Assert.AreEqual(expected.CreationDate.ToShortDateString(), actual.CreationDate.ToShortDateString());
        }

        [TestMethod]
        public void Delete_WhenObjectDeleted_ShouldNotReturnObject()
        {
            //arrange
            var dbContext = new TodoTasksDbContext();
            var repo = new TodoTasks.Data.TodoTasksData();

            TodoTask task = dbContext.Tasks.FirstOrDefault();
            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();
            repo.Tasks.Delete(task);

            //act
            var actual = repo.Tasks.All().FirstOrDefault(t => t.Id == task.Id);
            var expected = dbContext.Tasks.FirstOrDefault(t => t.Id == task.Id);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Find_WhenObjectIsInDb_ShouldReturnObject()
        {
            //arrange
            var task = this.GetValidTestTask();

            var dbContext = new TodoTasksDbContext();
            var repo = new TodoTasksData();

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            //act
            var taskInDb = repo.Tasks.Find(task.Id);

            //assert

            Assert.IsNotNull(taskInDb);
            Assert.AreEqual(task.Content, taskInDb.Content);
            Assert.AreEqual(task.CreationDate.ToShortDateString(), taskInDb.CreationDate.ToShortDateString());
        }


        private TodoTask GetValidTestTask()
        {
            var dbContext = new TodoTasksDbContext();
            var ran = new Random();

            var task = new TodoTask()
            {
                Content = "Test task " + ran.Next(0, 100),
                CreationDate = DateTime.Now,
                Status = StatusType.Incompleted,
                CategoryId = dbContext.Categories.First().Id
            };
            return task;
        }

    }
}

