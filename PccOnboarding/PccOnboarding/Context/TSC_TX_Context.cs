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
    public class TSC_TX_Context : DbContext
    {

        public DbSet<ClientInfoTable> ClientInfoTable { get; set; }
        public DbSet<PccPatientsClientTable> PccPatientsClientTable { get; set; }
        public DbSet<FacilitiesTable> FacilitiesTable { get; set; }
        public DbSet<PccFacilitiesTable> PccFacilitiesTable { get; set; }
        public DbSet<PccAdtTable> PccAdtTable { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_TX;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;");
            }
        }


    }
}
