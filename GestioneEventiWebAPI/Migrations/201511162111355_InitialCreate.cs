namespace GestioneEventiWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Eventi",
                c => new
                    {
                        IdEvento = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descrizione = c.String(nullable: false),
                        DescBreve = c.String(maxLength: 500),
                        NumeroPosti = c.Int(nullable: false),
                        UrlEvento = c.String(maxLength: 250),
                        UrlImmagine = c.String(maxLength: 250),
                        Inizio = c.DateTime(nullable: false),
                        Fine = c.DateTime(nullable: false),
                        IdLocation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEvento)
                .ForeignKey("dbo.Locations", t => t.IdLocation, cascadeDelete: true)
                .Index(t => t.IdLocation);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        IdLocation = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 250),
                        Descrizione = c.String(),
                        Capienza = c.Int(nullable: false),
                        Indirizzo_Via = c.String(maxLength: 150),
                        Indirizzo_NumeroCivico = c.String(maxLength: 20),
                        Indirizzo_Citta = c.String(maxLength: 50),
                        Indirizzo_Provincia = c.String(maxLength: 5),
                        Indirizzo_Cap = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.IdLocation);
            
            CreateTable(
                "dbo.Registrazioni",
                c => new
                    {
                        IdRegistrazione = c.Int(nullable: false, identity: true),
                        DataRichiestaRegistrazione = c.DateTime(nullable: false),
                        DataConfermaRegistrazione = c.DateTime(nullable: false),
                        DataUltimoStato = c.DateTime(nullable: false),
                        StatoRegistrazione = c.Int(nullable: false),
                        DataPartecipazione = c.DateTime(nullable: false),
                        Nota = c.String(),
                        IdEvento = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.IdRegistrazione);
            
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
                        Nome = c.String(nullable: false, maxLength: 100),
                        Cognome = c.String(nullable: false, maxLength: 100),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Eventi", "IdLocation", "dbo.Locations");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Eventi", new[] { "IdLocation" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Registrazioni");
            DropTable("dbo.Locations");
            DropTable("dbo.Eventi");
        }
    }
}
