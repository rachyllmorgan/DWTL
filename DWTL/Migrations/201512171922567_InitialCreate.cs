namespace DWTL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competitions",
                c => new
                    {
                        CompetitionId = c.Int(nullable: false, identity: true),
                        DownUserId = c.Int(nullable: false),
                        Name = c.String(),
                        CompType = c.Int(nullable: false),
                        Bet = c.Int(nullable: false),
                        Pot = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompetitionId);
            
            CreateTable(
                "dbo.DownUsers",
                c => new
                    {
                        DownUserId = c.Int(nullable: false, identity: true),
                        Handle = c.String(nullable: false, maxLength: 15),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Weight = c.Int(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.DownUserId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        DownUserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CompetitionId = c.Int(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Competitions", t => t.CompetitionId, cascadeDelete: true)
                .Index(t => t.CompetitionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DownUserCompetitions",
                c => new
                    {
                        DownUser_DownUserId = c.Int(nullable: false),
                        Competition_CompetitionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DownUser_DownUserId, t.Competition_CompetitionId })
                .ForeignKey("dbo.DownUsers", t => t.DownUser_DownUserId, cascadeDelete: true)
                .ForeignKey("dbo.Competitions", t => t.Competition_CompetitionId, cascadeDelete: true)
                .Index(t => t.DownUser_DownUserId)
                .Index(t => t.Competition_CompetitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Posts", "CompetitionId", "dbo.Competitions");
            DropForeignKey("dbo.DownUserCompetitions", "Competition_CompetitionId", "dbo.Competitions");
            DropForeignKey("dbo.DownUserCompetitions", "DownUser_DownUserId", "dbo.DownUsers");
            DropIndex("dbo.DownUserCompetitions", new[] { "Competition_CompetitionId" });
            DropIndex("dbo.DownUserCompetitions", new[] { "DownUser_DownUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Posts", new[] { "CompetitionId" });
            DropTable("dbo.DownUserCompetitions");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Posts");
            DropTable("dbo.DownUsers");
            DropTable("dbo.Competitions");
        }
    }
}