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
                },
                new Training()
                {
                    Description = "Full Body Workout",
                    DateTime = new DateTime(2020, 5, 8, 16, 20,00)
                },
                new Training()
                {
                    Description = "Cardio",
                    DateTime = new DateTime(2020, 5, 12, 12, 40,00)
                },
                new Training()
                {
                    Description = "Klatka",
                    DateTime = new DateTime(2020, 5, 13, 14, 30,00)
                },
                new Training()
                {
                    Description = "Nogi",
                    DateTime = new DateTime(2020, 5, 11, 19, 20,00)
                },
                new Training()
                {
                    Description = "Plecy",
                    DateTime = new DateTime(2020, 5, 12, 20, 00,00)
                },
                new Training()
                {
                    Description = "Full Body Workout",
                    DateTime = new DateTime(2020, 5, 8, 16, 20,00)
                },
                
            };
            var training2 = new List<Training>()
            {
                new Training
                {
                    Description = "Plecy",
                    DateTime = new DateTime(2020, 3, 27, 12, 00, 00),
                }
            };
            var training3 = new List<Training>()
            {
                new Training()
                {
                    Description = "Biceps",
                    DateTime = new DateTime(2020, 3, 27, 12, 00, 00),
                }
            };
            var training4 = new List<Training>()
            {
                new Training()
                {
                    Description = "Klatka",
                    DateTime = new DateTime(2020, 3, 27, 12, 00, 00),
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
                        Trainings = training1,
                        IsAddedByUser = false                        
                    },
                    new Gym
                    {
                        Name = "Body Fitness",
                        City = "Rabka Zdroj",
                        Street = "Pawia",
                        Number = "5",
                        Trainings = training2,
                        IsAddedByUser = false

                    },
                    new Gym
                    {
                        Name = "Fitness Platinum",
                        City = "Sosnowiec",
                        Street = "Łokietka",
                        Number = "15/2",
                        Trainings = training3,
                        IsAddedByUser = false

                    },
                    new Gym
                    {
                        Name = "Pure Gyn",
                        City = "Warszawa",
                        Street = "Raciborska",
                        Number = "74",
                        Trainings = training4,
                        IsAddedByUser = false

                    });
            }

            context.SaveChanges();

        }
    }
}