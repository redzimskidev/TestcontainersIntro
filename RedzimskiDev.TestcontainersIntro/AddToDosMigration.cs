using FluentMigrator;

namespace RedzimskiDev.TestcontainersIntro
{
    [Migration(20221203)]
    public class AddToDosMigration : Migration
    {
        public override void Up()
        {
            Create.Table("ToDos")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Content").AsString();
        }

        public override void Down()
        {
            Delete.Table("ToDos");
        }
    }
}