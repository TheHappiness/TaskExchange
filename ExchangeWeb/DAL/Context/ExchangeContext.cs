using ExchangeWeb.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ExchangeWeb.DAL.Context
{
    public class ExchangeContext : DbContext
    {
        static ExchangeContext()
        {
            Database.SetInitializer<ExchangeContext>(new ExchangeDbInitializer());
        }

        public ExchangeContext()
            : base("ExchangeConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Participant>()
            //    .HasMany(p => p.Customers)
            //    .WithRequired(c => c.Customer)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Participant>()
            //    .HasMany(p => p.Sellers)
            //    .WithRequired(c => c.Seller)
            //    .WillCascadeOnDelete(false);
        }

        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
    }
}