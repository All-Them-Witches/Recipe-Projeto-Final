namespace Domain.Entities
{
    /// <summary>
    /// Representa uma avaliação de uma receita.
    /// </summary>
    public class Review
    {
        public int Id { get; private set; }
        public int Rating { get; private set; } // Avaliação de 1 a 5 estrelas, por exemplo.
        public string Comment { get; private set; }
        public int RecipeId { get; private set; }
        public Recipe Recipe { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        private Review() { }

        public Review(int rating, string comment, int recipeId, int userId)
        {
            Rating = rating;
            Comment = comment;
            RecipeId = recipeId;
            UserId = userId;
        }

        public void UpdateReview(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
        }
    }

    /// <summary>
    /// Representa um comentário em uma receita.
    /// </summary>
    public class Comment
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public int RecipeId { get; private set; }
        public Recipe Recipe { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        private Comment() { }

        public Comment(string text, int recipeId, int userId)
        {
            Text = text;
            RecipeId = recipeId;
            UserId = userId;
        }

        public void UpdateComment(string text)
        {
            Text = text;
        }
    }
}
