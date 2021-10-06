namespace complaint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        AdminEmail = c.String(unicode: false),
                        Password = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 500, unicode: false),
                        CompanyID = c.Int(nullable: false),
                        ComplaintID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.Complaints", t => t.ComplaintID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.ComplaintID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 150, unicode: false),
                        NameSurname = c.String(nullable: false, maxLength: 30, unicode: false),
                        Dissolved = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false, unicode: false),
                        TotalComplaint = c.Int(nullable: false),
                        WebUrl = c.String(maxLength: 150, unicode: false),
                        EPosta = c.String(nullable: false, maxLength: 150, unicode: false),
                        Case = c.Boolean(nullable: false),
                        CompanyLogo = c.String(maxLength: 150, unicode: false),
                        CompanyPassword = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        ComplaintID = c.Int(nullable: false, identity: true),
                        ComplainDetail = c.String(nullable: false, maxLength: 650, unicode: false),
                        Result = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Case = c.Boolean(nullable: false),
                        Document = c.String(maxLength: 250, unicode: false),
                        ComplaintTitle = c.String(nullable: false, maxLength: 250, unicode: false),
                        CompanyID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComplaintID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 500, unicode: false),
                        Image = c.String(maxLength: 200, unicode: false),
                        UserID = c.Int(nullable: false),
                        ComplaintID = c.Int(nullable: false),
                        Case = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Complaints", t => t.ComplaintID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ComplaintID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserNameSurname = c.String(nullable: false, maxLength: 80, unicode: false),
                        UserImage = c.String(maxLength: 250, unicode: false),
                        EMail = c.String(nullable: false, maxLength: 100, unicode: false),
                        UserPassword = c.String(nullable: false, maxLength: 30, unicode: false),
                        Role = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complaints", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Complaints", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "ComplaintID", "dbo.Complaints");
            DropForeignKey("dbo.Answers", "ComplaintID", "dbo.Complaints");
            DropForeignKey("dbo.Answers", "CompanyID", "dbo.Companies");
            DropIndex("dbo.Comments", new[] { "ComplaintID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Complaints", new[] { "UserID" });
            DropIndex("dbo.Complaints", new[] { "CompanyID" });
            DropIndex("dbo.Answers", new[] { "ComplaintID" });
            DropIndex("dbo.Answers", new[] { "CompanyID" });
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Complaints");
            DropTable("dbo.Companies");
            DropTable("dbo.Answers");
            DropTable("dbo.Admins");
        }
    }
}
