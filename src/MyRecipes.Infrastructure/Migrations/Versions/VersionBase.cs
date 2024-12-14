using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MyRecipes.Infrastructure.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{
    public ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string name)
    {
        return Create.Table(name)
           .WithColumn("Id").AsInt64().PrimaryKey().Identity()
           .WithColumn("CreatedOn").AsDateTime().NotNullable()
           .WithColumn("Active").AsBoolean().NotNullable();
    }
}
