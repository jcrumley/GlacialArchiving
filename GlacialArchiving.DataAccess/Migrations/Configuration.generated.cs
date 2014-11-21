using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;

namespace GlacialArchiving.DataAccess.Migrations
{
    internal sealed partial class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            this.OnLoad();
        }
        partial void OnLoad();

        protected override void Seed(DataContext context)
        {
            this.BeforeSeeding(context);
            Seeding seed = new Seeding();            
            seed.SeedBase(context);
            this.AfterSeeding(context);
        }
        partial void BeforeSeeding(DataContext context);
        partial void AfterSeeding(DataContext context);
    }
}