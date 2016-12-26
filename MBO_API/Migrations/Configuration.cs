namespace MBO_API.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using MBO_API.Models;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MBO_API.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MBO_API.Models.ApplicationDbContext context)
        {
            context.Category.AddRange(new List<Category> {
                new Category { Title="Recruitment", Description="Recruitment"},
                new Category { Title="Training", Description="Training"},
                new Category { Title="Placement", Description="Placement"}
            });

            context.Status.AddRange(new List<Status>
            {
                new Status {Level=0 , Title="Assigned", Description="Task has been created" },
                new Status {Level=1 , Title="Acknowledged", Description="Task has been acknowledged" },
                new Status {Level=2 , Title="In-Process", Description="Task in process" },
                new Status {Level=3 , Title="On-Hold", Description="Task in On-Hold" },
                new Status {Level=4 , Title="Require Input", Description="Task require input from other party" },
                new Status {Level=5 , Title="Completed", Description="Task completed" }
            });
        }
    }
}
