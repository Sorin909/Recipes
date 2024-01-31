namespace Recipes_SQL.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Summary { get; set; }
        public string Ingredients { get; set; }
        public string Method { get; set; }
        public string ImageUrl { get; set; }
    }
}
