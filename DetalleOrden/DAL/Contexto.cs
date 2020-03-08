using System;
using System.Collections.Generic;
using System.Text;
using DetalleOrden.Entidades;
using Microsoft.EntityFrameworkCore;

namespace DetalleOrden.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Productos> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source = Database\DetalleOrden.db");
        }
    }
}
