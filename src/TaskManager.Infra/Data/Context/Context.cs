﻿using Microsoft.EntityFrameworkCore;

namespace TaskManager.Infra.Data
{
    public class Context : DbContext
    {
        protected Context()
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(1000)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

            base.OnModelCreating(modelBuilder);
        }
    }
}
