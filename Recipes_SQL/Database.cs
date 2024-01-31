using Recipes_SQL.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Database
{
    internal class Database
    {
        private readonly string connectionString = "Data Source=SORING-DATOR2;Initial Catalog=RecipesDB;Integrated Security=True;Encrypt=False";

        public List<Recipe> GetRecipes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Recept";

                var recipes = new List<Recipe>();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int recipeId = int.Parse(reader["Id"].ToString()); // Change 'id' to 'recipeId'
                        string name = reader["Name"].ToString();
                        string summary = reader["Summary"].ToString();
                        string ingredients = reader["Ingredients"].ToString();
                        string method = reader["Method"].ToString();
                        string imageUrl = reader["ImageUrl"].ToString();

                        recipes.Add(new Recipe()
                        {
                            Name = name,
                            Id = recipeId, // Use the new variable name here
                            Summary = summary,
                            Ingredients = ingredients,
                            Method = method,
                            ImageUrl = imageUrl
                        });
                    }
                }

                return recipes;
            }
        }
        public Recipe GetRecipeById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Recept WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int recipeId = int.Parse(reader["Id"].ToString());
                        string name = reader["Name"].ToString();
                        string summary = reader["Summary"].ToString();
                        string ingredients = reader["Ingredients"].ToString();
                        string method = reader["Method"].ToString();
                        string imageUrl = reader["ImageUrl"].ToString();

                        return new Recipe()
                        {
                            Name = name,
                            Id = recipeId,
                            Summary = summary,
                            Ingredients = ingredients,
                            Method = method,
                            ImageUrl = imageUrl
                        };
                    }
                }

                return null; 
            }
        }

        public void SaveRecipe(string name, string summary, string ingredients, string method, string imageUrl)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Recept (Name, Summary, Ingredients, Method, ImageUrl) " +
                                  "VALUES (@Name, @Summary, @Ingredients, @Method, @ImageUrl)";

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Summary", summary);
                cmd.Parameters.AddWithValue("@Ingredients", ingredients);
                cmd.Parameters.AddWithValue("@Method", method);
                cmd.Parameters.AddWithValue("@ImageUrl", imageUrl);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateRecipe(Recipe recipe)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Recept " +
                                  "SET Name = @Name, Summary = @Summary, Ingredients = @Ingredients, " +
                                  "Method = @Method, ImageUrl = @ImageUrl " +
                                  "WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Name", recipe.Name);
                cmd.Parameters.AddWithValue("@Summary", recipe.Summary);
                cmd.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                cmd.Parameters.AddWithValue("@Method", recipe.Method);
                cmd.Parameters.AddWithValue("@ImageUrl", recipe.ImageUrl);
                cmd.Parameters.AddWithValue("@Id", recipe.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRecipe(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Recept WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
