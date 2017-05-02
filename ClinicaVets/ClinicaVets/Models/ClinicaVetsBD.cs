using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ClinicaVets.Models
{
    public class ClinicaVetsBD : DbContext
    {
        //representar a Bade se dados
        //descrevendo as tabelas que lá estao contidas

        //representar o 'construtor' desta classe 
        //identifica onde se encontra a Base de Dados 
        public ClinicaVetsBD() : base("DefaultConnection") { }

        //descrever as tabelas que estao na base de dados 
        public virtual DbSet<Donos> Donos { get; set; }
        public virtual DbSet<Animais> Animais { get; set; }
        public virtual DbSet<Veterinarios> Veterinarios { get; set; }
        public virtual DbSet<Consultas> Consultas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // não podemos usar a chave seguinte, nesta geração de tabelas
            // por causa das tabelas do Identity (gestão de utilizadores)
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}