﻿

using Base_Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server_Library.Data
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<Employee> EmployeesTbl { get; set; }
		public DbSet<GeneralDep> GeneralDepTbl { get; set; }
		public DbSet<Departtment> DeparttmentTbl { get; set; }
		public DbSet<Branch> BranchesTbl { get; set; }
		public DbSet<Town> TownsTbl { get; set; }
		public DbSet<AppUser> AppUsersTbl { get; set; }

	}
}
