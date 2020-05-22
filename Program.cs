using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw11.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace cw11
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //InsertExamples();
            CreateHostBuilder(args).Build().Run();
            
        }

        public static void InsertExamples()
        {
            var db = new CodeFirstContext();

            var pm = new PrescriptionMedicament()
            {
                Dose = 200,
                Details = "Dwa razy dziennie"
            };
            var set1 = new HashSet<PrescriptionMedicament> { pm };

            var p = new Prescription()
            {
                //IdPrescription = 1,
                Date = DateTime.Now,
                DueDate = DateTime.Now,
                PrescriptionMedicament = set1
            };
            var set = new HashSet<Prescription> { p };

            var d = new Doctor
            {
                //IdDoctor = 1,
                FirstName = "Jakub",
                LastName = "Bogus³awski",
                Email = "s18312@pjwstk.edu.pl",
                Prescription = set
            };

            var pt = new Patient()
            {
                //IdPatient = 1,
                FirstName = "Barbara",
                LastName = "Bak³a¿an",
                Birthdate = DateTime.Parse("Jan 1, 2000"),
                Prescription = set
            };

            

            var m = new Medicament()
            {
                //IdMedicament = 1,
                Name = "Theraflu",
                Description = "Na katar",
                Type = "Bez recepty",
                PrescriptionMedicament = set1
            };

           


            db.Doctor.Add(d);
            db.Patient.Add(pt);
            db.Medicament.Add(m);
            db.Prescription.Add(p);
            

            db.SaveChanges();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
