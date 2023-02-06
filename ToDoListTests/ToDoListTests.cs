using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoListApp.Enums;
using ToDoListApp.Pages.ToDoItems;
using ToDoListApp.Services;
using ToDoListApp.ViewModels;

namespace ToDoListTests
{
    [TestClass]
    public class ToDoListTests
    {
        private static ToDoItemService toDoItemServicePrep;

        [TestMethod]
        public async Task CreateToDoItemTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoDbContext>().UseInMemoryDatabase("InMemoryDb");

            using (var db = new ToDoDbContext(optionsBuilder.Options))
            {
                await PrepareData(db);

                var todoItem = db.ToDoItems.FirstOrDefault();

                if (todoItem != null)
                {
                    Assert.AreEqual(todoItem.Name, "Name");
                    Assert.AreEqual(todoItem.Priority, 1);
                    Assert.AreEqual(todoItem.Status, ToDoItemState.Completed);
                }
            }
        }

        [TestMethod]
        public async Task EditToDoItemTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoDbContext>().UseInMemoryDatabase("InMemoryDb");

            using (var db = new ToDoDbContext(optionsBuilder.Options))
            {
                await PrepareData(db);

                var editService = new ToDoItemEditService(db);
                var id = db.ToDoItems.FirstOrDefault()?.Id;
                if(id == null)
                {
                    Assert.Fail();
                }

                var model = await editService.LoadModelAsync((Guid)id);
                model.Name= "New Name";
                model.Priority= 2;

                await editService.EditAsync(model);

                var editedItem = db.ToDoItems.FirstOrDefault();
                Assert.AreEqual(editedItem.Name, "New Name");
                Assert.AreEqual(editedItem.Priority, 2);
                Assert.AreEqual(editedItem.Status, ToDoItemState.Completed);
            }
        }

        [TestMethod]
        public async Task DeleteCompletedToDoItemTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoDbContext>().UseInMemoryDatabase("InMemoryDb");

            using (var db = new ToDoDbContext(optionsBuilder.Options))
            {
                await PrepareData(db);

                Assert.AreEqual(db.ToDoItems.Count(), 1);

                await toDoItemServicePrep.DeleteAsync();

                Assert.AreEqual(db.ToDoItems.Count(), 0);
            }
        }

        private static async Task PrepareData(ToDoDbContext db)
        {

            db.Database.EnsureCreated();
            toDoItemServicePrep = new ToDoItemService(db);
            if (db.ToDoItems.Any())
            {
                return;
            }
            var createModel = new CreateModel(db, toDoItemServicePrep);
            createModel.ToDoItemModel = new ToDoItemViewModel
            {
                Name = "Name",
                Priority = 1,
                Status = ToDoItemState.Completed
            };
            await toDoItemServicePrep.CreateAsync(createModel.ToDoItemModel);
        }
    }
}