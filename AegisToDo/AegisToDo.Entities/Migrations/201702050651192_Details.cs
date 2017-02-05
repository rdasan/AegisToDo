namespace AegisToDo.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Details : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoItems", "Details", c => c.String());
            AddColumn("dbo.ToDoItems", "DueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoItems", "DueDate");
            DropColumn("dbo.ToDoItems", "Details");
        }
    }
}
