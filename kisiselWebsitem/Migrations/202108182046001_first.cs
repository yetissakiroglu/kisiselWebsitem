namespace kisiselWebsitem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ayarlar", "Aciklama", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ayarlar", "Aciklama");
        }
    }
}
