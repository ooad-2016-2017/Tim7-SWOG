﻿using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SPARK.Model
{
    public class SPARKDbContext : DbContext
    {
        //Svi restorani koji su u tabeli se dobijaju iz ovog seta
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }



        //Metoda koja će promijeniti konfiguraciju i odrediti gdje se spašava klasa i kako se zove.
        //Ovisno od uređaja spasiti će se na različite lokacije, za desktop se kreira poseban folder u AppData/Local Folderu od korisnika
        //Svaki korisnik koji pokrene aplikaciju će imati kreiranu bazu lokalno kod sebe
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = "Ooadbaza.db";
            try
            {
                //za tačnu putanju gdje se nalazi baza uraditi ovdje debug i procitati Path
                databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
            }
            catch (InvalidOperationException) { }
            //Sqlite baza
            optionsBuilder.UseSqlite($"Data source={databaseFilePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>().Property()



                  modelBuilder.Entity<Publisher>().HasKey(p => p.PublisherId);
            modelBuilder.Entity<Publisher>().Property(c => c.PublisherId)
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<Book>().Property(b => b.BookId)
                  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Book>().HasRequired(p => p.Publisher)
                  .WithMany(b => b.Books).HasForeignKey(b => b.PublisherId);
            base.OnModelCreating(modelBuilder);

        }
    }
}
}
