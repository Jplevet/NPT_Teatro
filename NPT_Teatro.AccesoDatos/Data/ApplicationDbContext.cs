using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NPT_Teatro.Models;

namespace NPT_Teatro.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        public DbSet<Obra> Obra { get; set; }

        public DbSet<Funcion> Funcion { get; set; }

        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
