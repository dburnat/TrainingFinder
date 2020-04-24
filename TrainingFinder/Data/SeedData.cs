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
            if (!context.Trainings.Any())
            {
                context.Trainings.AddRange(
                    new Training
                    {
                        Description = "Klata",
                        DateTime = new DateTime(2020, 3, 27, 10, 0, 0),
                        GymId = 3
                    },
                    new Training
                    {
                        Description = "Plecy",
                        DateTime = new DateTime(2020, 3, 27, 12, 0, 0),
                        GymId = 4
                    },
                    new Training
                    {
                        Description = "Barki",
                        DateTime = new DateTime(2020, 3, 27, 11, 30, 0),
                        GymId = 2
                    },
                    new Training
                    {
                        Description = "Triceps",
                        DateTime = new DateTime(2020, 3, 27, 17, 45, 0),
                        GymId = 2
                    }); ;
            }
            if (!context.Gyms.Any())
            {
                context.Gyms.AddRange(
                    new Gym
                    {
                        Name = "Fitness Land",
                        City = "Krakow",
                        Street = "al. Adama Mickiewicza",
                        Number = "34/2"
                    },
                    new Gym
                    {
                        Name = "Body Fitness",
                        City = "Rabka Zdroj",
                        Street = "Pawia",
                        Number = "5"

                    },
                    new Gym
                    {
                        Name = "Fitness Platinum",
                        City = "Sosnowiec",
                        Street = "Łokietka",
                        Number = "15/2"

                    },
                    new Gym
                    {
                        Name = "Pure Gyn",
                        City = "Warszawa",
                        Street = "Raciborska",
                        Number = "74"

                    });
            }

            context.SaveChanges();

        }
    }
}