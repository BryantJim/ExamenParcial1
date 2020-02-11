using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ExamenParcial1.Entidades;

namespace ExamenParcial1.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Productos> Producto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=BRYANTPC\SQLEXPRESS;Database=ProductosDb;Trusted_Connection=True;");
        }
    }
}
