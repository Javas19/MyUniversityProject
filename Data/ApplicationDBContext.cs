using DigeraitMIS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigeraitMIS.Models.ViewModels;

namespace DigeraitMIS.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Transaction> tblTransaction { get; set; }
        public DbSet<CentreProfile> tblCentre { get; set; }
        public DbSet<Region> tblRegion { get; set; }
        public DbSet<Manager> tblManager { get; set; }
        public DbSet<User> tblUser { get; set; }
        public DbSet<Income> tblIncome { get; set; }
        public DbSet<Expenditure> tblExpenditure { get; set; }
        public DbSet<City> tblCity { get; set; }
        public DbSet<Province> tblProvince { get; set; }
        public DbSet<Teacher> tblTeacher { get; set; }
        //add the db
        public DbSet<Suggestions> tblSuggestions { get; set; }
        public DbSet<Pupil> tblPupil { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<DigeraitMIS.Models.ViewModels.TransactionVM> TransactionVM { get; set; }

        public DbSet<DigeraitMIS.Models.ViewModels.CentreProfileVM> CentreProfileVM { get; set; }

        public DbSet<DigeraitMIS.Models.ViewModels.ManageStaffVM> ManageStaffVM { get; set; }
    }
}
