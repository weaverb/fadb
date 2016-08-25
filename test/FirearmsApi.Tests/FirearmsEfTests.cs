using fadb_api.ef;
using fadb_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace FirearmsApi.Tests
{
    public class Firearms_Db_Should
    {
        private static DbContextOptions<FirearmDbContext> CreateContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<FirearmDbContext>();
            builder.UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }


        [Fact]
        public void WriteToDbWhenAddIsCalled()
        {
            var options = CreateContextOptions();

            using (var context = new FirearmDbContext(options))
            {
                var repo = new EfFirearmRepository(context);
                repo.Add(new Firearm { Name = "Test" });
            }

            using (var context = new FirearmDbContext(options))
            {
                Assert.Equal(1, context.Firearms.Count());
                Assert.Equal("Test", context.Firearms.Single().Name);
            }
        }


        [Fact]
        public void ReturnAllFirearmsWhenGetAllIsCalled()
        {
            var options = CreateContextOptions();

            using (var context = new FirearmDbContext(options))
            {
                context.Firearms.Add(new Firearm { Name = "Test 1" });
                context.Firearms.Add(new Firearm { Name = "Test 2" });
                context.Firearms.Add(new Firearm { Name = "Test 3" });
                context.Firearms.Add(new Firearm { Name = "Test 4" });
                context.Firearms.Add(new Firearm { Name = "Test 5" });
                context.SaveChanges();
            }

            using (var context = new FirearmDbContext(options))
            {
                var repo = new EfFirearmRepository(context);
                var result = repo.GetAll();
                Assert.Equal(5, result.Count());
            }
        }

        [Fact]
        public void ReturnSelectedFirearmWhenFindIsCalled()
        {
            var options = CreateContextOptions();

            using (var context = new FirearmDbContext(options))
            {
                context.Firearms.Add(new Firearm { Id = 1, Name = "Test 1" });
                context.Firearms.Add(new Firearm { Id = 2, Name = "Test 2" });
                context.Firearms.Add(new Firearm { Id = 3, Name = "Test 3" });
                context.Firearms.Add(new Firearm { Id = 4, Name = "Test 4" });
                context.Firearms.Add(new Firearm { Id = 5, Name = "Test 5" });
                context.SaveChanges();
            }

            using (var context = new FirearmDbContext(options))
            {
                var repo = new EfFirearmRepository(context);
                var result = repo.Find(3);
                Assert.Equal("Test 3", result.Name);
            }
        }

        [Fact]
        public void ReduceCountByOneWhenRemoveIsCalled()
        {
            var options = CreateContextOptions();

            using (var context = new FirearmDbContext(options))
            {
                context.Firearms.Add(new Firearm { Id = 1, Name = "Test 1" });
                context.Firearms.Add(new Firearm { Id = 2, Name = "Test 2" });
                context.Firearms.Add(new Firearm { Id = 3, Name = "Test 3" });
                context.Firearms.Add(new Firearm { Id = 4, Name = "Test 4" });
                context.Firearms.Add(new Firearm { Id = 5, Name = "Test 5" });
                context.SaveChanges();
            }

            using (var context = new FirearmDbContext(options))
            {
                var repo = new EfFirearmRepository(context);
                var removed = repo.Remove(3);
                Assert.Equal("Test 3", removed.Name);
                var results = repo.GetAll();
                Assert.Equal(4, results.Count());
                var result = repo.Find(3);
                Assert.Null(result);
            }
        }

        [Fact]
        public void FirearmUpdatedWhenUpdateIsCalled()
        {
            var options = CreateContextOptions();

            using (var context = new FirearmDbContext(options))
            {
                context.Firearms.Add(new Firearm { Id = 1, Name = "Test 1" });
                context.SaveChanges();
            }

            using (var context = new FirearmDbContext(options))
            {
                var repo = new EfFirearmRepository(context);
                repo.Update(new Firearm { Id = 1, Name = "Updated" });
                var result = repo.Find(1);
                Assert.Equal("Updated", result.Name);

            }
        }
    }
}
