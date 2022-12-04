using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using RedzimskiDev.TestcontainersIntro;

namespace TestcontainersIntro.Tests
{
    public class ToDoRepositoryTests : IAsyncLifetime
    {
        private MsSqlTestcontainer _sqlContainer;

        public ToDoRepositoryTests()
        {
            _sqlContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(new MsSqlTestcontainerConfiguration
                {
                    Password = "Your_password123"
                })
                .Build();
        }

        [Fact]
        public void Add_ShouldSaveToDoToDatabase()
        {
            var repository = new ToDoRepository(_sqlContainer.ConnectionString);
            var toDoToAdd = new ToDo("ToDoContent");

            repository.Add(toDoToAdd);
            var addedToDo = repository.Get(toDoToAdd.Id);

            Assert.NotNull(addedToDo);
        }

        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();

            Migrate();
        }

        public async Task DisposeAsync()
        {
            await _sqlContainer.StopAsync();
        }

        private void Migrate()
        {
            var serviceCollection = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddSqlServer()
                        .WithGlobalConnectionString(_sqlContainer.ConnectionString)
                        .WithMigrationsIn(typeof(AddToDosMigration).Assembly))
                .BuildServiceProvider();

            var runner = serviceCollection.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}