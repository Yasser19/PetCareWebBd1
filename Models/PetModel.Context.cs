﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetCareBd1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PETCAREEntities : DbContext
    {
        public PETCAREEntities()
            : base("name=PETCAREEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bill> bill { get; set; }
        public virtual DbSet<customers> customers { get; set; }
        public virtual DbSet<employees> employees { get; set; }
        public virtual DbSet<pets> pets { get; set; }
        public virtual DbSet<serviceType> serviceType { get; set; }
        public virtual DbSet<t_services> t_services { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<userstype> userstype { get; set; }
    }
}