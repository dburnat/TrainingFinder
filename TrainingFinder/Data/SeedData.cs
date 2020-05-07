using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();

            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            
            var training1 = new List<Training>()
            {
                new Training()
                {
                    Description = "Triceps",
                    DateTime = new DateTime(2020, 3, 27, 17, 45, 0),
                }
            };
            var training2 = new List<Training>()
            {
                new Training
                {
                    Description = "Plecy",
                    DateTime = new DateTime(2020, 3, 27, 12, 0, 0),
                }
            };
            var training3 = new List<Training>()
            {
                new Training()
                {
                    Description = "Biceps",
                    DateTime = new DateTime(2020, 3, 27, 12, 0, 0),
                }
            };
            var training4 = new List<Training>()
            {
                new Training()
                {
                    Description = "Klatka",
                    DateTime = new DateTime(2020, 3, 27, 12, 0, 0),
                }
            };
            
            if (!context.Gyms.Any())
            {
                context.Gyms.AddRange(
                    new Gym
                    {
                        Name = "Fitness Land",
                        City = "Krakow",
                        Street = "al. Adama Mickiewicza",
                        Number = "34/2",
                        Trainings = training1
                    },
                    new Gym
                    {
                        Name = "Body Fitness",
                        City = "Rabka Zdroj",
                        Street = "Pawia",
                        Number = "5",
                        Trainings = training2

                    },
                    new Gym
                    {
                        Name = "Fitness Platinum",
                        City = "Sosnowiec",
                        Street = "Łokietka",
                        Number = "15/2",
                        Trainings = training3
                    },
                    new Gym
                    {
                        Name = "Pure Gyn",
                        City = "Warszawa",
                        Street = "Raciborska",
                        Number = "74",
                        Trainings = training4

                    });
            }

            context.SaveChanges();

        }
    }
}