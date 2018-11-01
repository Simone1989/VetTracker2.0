namespace VetTracker2.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VetTracker2.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<VetTracker2.DataAccess.VetTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VetTracker2.DataAccess.VetTrackerContext context)
        {
            context.Pets.AddOrUpdate(
                p => p.Name,
                new Pet { Type = "Golden retriever", Name = "Doggo", Illness = "Broken tail", Owner = "Malfred McBook" },
                new Pet { Type = "Llama", Name = "Max", Illness = "Influensa", Owner = "Vincent Buck" },
                new Pet { Type = "Cat", Name = "BoatDude", Illness = "Blindness", Owner = "Rita Barker" },
                new Pet { Type = "Parrot", Name = "Francis", Illness = "Aspergillus", Owner = "Meagan Gallagher" },
                new Pet { Type = "Chinchilla", Name = "Rocket", Illness = "Rabies", Owner = "Frank England" },
                new Pet { Type = "Guinea pig", Name = "Sara", Illness = "Thorn in paw", Owner = "Leonard Woods" },
                new Pet { Type = "Horse", Name = "Stud", Illness = "Bruises on leg", Owner = "Alexandra Baxter" }
            );
        }
    }
}
