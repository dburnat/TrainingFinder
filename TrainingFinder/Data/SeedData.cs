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
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Trainings.Any())
            {
                context.Trainings.AddRange(
                    new Training
                    {
                        Description = "Klata",
                        DateTime = new DateTime(2020, 3, 27, 10, 0, 0)
                    },
                    new Training
                    {
                        Description = "Plecy",
                        DateTime = new DateTime(2020, 3, 27, 12, 0, 0)
                    },
                    new Training
                    {
                        Description = "Barki",
                        DateTime = new DateTime(2020, 3, 27, 11, 30, 0)
                    },
                    new Training
                    {
                        Description = "Triceps",
                        DateTime = new DateTime(2020, 3, 27, 17, 45, 0)
                    });
            }
            if (!context.Gyms.Any())
            {
                context.Gyms.AddRange(
                    new Gym
                    {
                        Name = "Fitness Land",
                        Address = "Krakow",
                        Latitude = "30",
                        Longitude = "10"
                    },
                    new Gym
                    {
                        Name = "Body Fitness",
                        Address = "Rabka Zdroj",
                        Latitude = "30",
                        Longitude = "10"
                    },
                    new Gym
                    {
                        Name = "Fitness Platinum",
                        Address = "Sosnowiec",
                        Latitude = "30",
                        Longitude = "10"
                    },
                    new Gym
                    {
                        Name = "Pure Gyn",
                        Address = "Warszawa",
                        Latitude = "30",
                        Longitude = "10"
                    });
            }

            context.SaveChanges();

        }
    }
}
