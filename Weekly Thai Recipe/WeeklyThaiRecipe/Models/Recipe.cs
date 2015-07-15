namespace WeeklyThaiRecipe.Models
{
    using System;
    using System.Collections.Generic;

    public class Recipe
    {
        public int Id { get; set; }

        public string Language { get; set; }

        public int Week { get; set; }

        public int Year { get; set; }

        public string Title { get; set; }

        public string Directions { get; set; }

        public string Image { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookTime { get; set; }

        public List<string> Ingredients { get; set; }

        public Spicy Spiciness { get; set; }

        public int ForNumberOfPersons { get; set; }
    }
}