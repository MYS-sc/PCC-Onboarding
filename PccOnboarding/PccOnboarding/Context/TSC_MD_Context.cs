﻿using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PccOnboarding.Context
{

    public class TSC_MD_Context(DbContextOptions<TSC_MD_Context> options) : DbContext(options)
    {


        public DbSet<ClientInfoTable> ClientInfoTable { get; set; }
        public DbSet<PccPatientsClientTable> PccPatientsClientTable { get; set; }
        public DbSet<FacilitiesTable> FacilitiesTable { get; set; }
        public DbSet<PccFacilitiesTable> PccFacilitiesTable { get; set; }
        public DbSet<PccAdtTable> PccAdtTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
