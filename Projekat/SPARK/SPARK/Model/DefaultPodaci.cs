using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPARK.Model
{
    public class DefaultPodaci
    {
        public static void Initialize(SPARKDbContext context)
        {
            if (!context.Parkings.Any())
            {
                Parking p = new Parking();
                p.Id = 0;
                p.Address = "Zmaja od Bosne";
                p.Name = "Defaultni parking";
                p.Zone = 1;
                p.Price = 2;
                p.CoordX = 43.865;
                p.CoordY = 18.413;
                p.Capacity = 10;
                p.NumTakenSpaces = 20;
                p.WorkingHours = new KeyValuePair<DateTime, DateTime>(Convert.ToDateTime("1.1.2017"), Convert.ToDateTime("5.2.2017"));
                p.TodaysProfit = 100;
                p.MonthlyProfit = 1000;
                p.NumTakenSpaces = 50;
                p.MinCredits = 10;
                context.Parkings.AddRange(p);
                context.SaveChanges();
            }

            if (!context.User.Any())
            {
                User u = new User();
                u.Id = 0;
                u.Username = "Zubatroun";
                u.Password = "12345678";
                u.Name = "Rifet";
                u.Surname = "Auspuh";
                u.Email = "email@email.com";
                context.User.AddRange(u);
                context.SaveChanges();
            }

            if (!context.Owner.Any())
            {
                Owner u = new Owner();
                u.Id = 0;
                u.Username = "Leksi";
                u.Password = "12345678";
                u.Name = "Imeneko";
                u.Surname = "Prezimeneko";
                u.Email = "email@email.com";
                context.Owner.AddRange(u);
                context.SaveChanges();
            }

            if (!context.PaymentMethods.Any())
            {
                PaymentMethod u = new PaymentMethod();
                u.Id = 0;
                u.Name = "Kartica";
                u.Description = "Placanje kreditnom karticom";
                u.Packages = new List<KeyValuePair<int, double>> { new KeyValuePair<int, double>(1, 20) };

                context.PaymentMethods.AddRange(u);
                context.SaveChanges();
            }
        }
    }

}

