namespace ZenithWebSite.Migrations.IdentityMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ZenithDataLib.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<ZenithWebSite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\IdentityMigrations";
        }

        protected override void Seed(ZenithWebSite.Models.ApplicationDbContext context)
        {
            context.Activities.AddOrUpdate(i => i.ActivityId,

                   new Activity
                   {
                       ActivityId = 1,
                       ActivityDescription = "soccer",
                       CreationData = new DateTime(2017, 1, 22, 8, 0, 0),
                       EventId = 1

                   },
                   new Activity
                   {
                       ActivityId = 2,
                       ActivityDescription = "basketball",
                       CreationData = new DateTime(2017, 1, 22, 8, 0, 0),
                       EventId = 2

                   },
                   new Activity
                   {
                       ActivityId = 3,
                       ActivityDescription = "vollyball",
                       CreationData = new DateTime(2017, 1, 22, 8, 0, 0),
                       EventId = 3

                   }
           );

            context.Events.AddOrUpdate(i => i.EventId,

                    new Event
                    {
                        EventId = 1,
                        EventFromDateAndTime = new DateTime(2017, 2, 22, 8, 0, 0),
                        EventToDateAndTime = new DateTime(2017, 2, 22, 10, 0, 0),
                        CreationDate = new DateTime(2017, 1, 22, 8, 0, 0),
                        IsActive = true
                    },
                    new Event
                    {
                        EventId = 2,
                        EventFromDateAndTime = new DateTime(2017, 2, 23, 8, 0, 0),
                        EventToDateAndTime = new DateTime(2017, 2, 24, 10, 0, 0),
                        CreationDate = new DateTime(2017, 1, 22, 8, 0, 0),
                        IsActive = true
                    },
                    new Event
                    {
                        EventId = 3,
                        EventFromDateAndTime = new DateTime(2017, 2, 24, 8, 0, 0),
                        EventToDateAndTime = new DateTime(2017, 2, 24, 10, 0, 0),
                        CreationDate = new DateTime(2017, 1, 22, 8, 0, 0),
                        IsActive = false
                    }
            );

            

        }
    }
}
