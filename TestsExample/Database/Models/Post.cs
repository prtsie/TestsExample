namespace TestsExample.Database.Models
{
    /// <summary> Пост </summary>
    public class Post
    {
        /// <summary> Идентификатор </summary>
        public Guid Id { get; set; }

        /// <summary> Заголовок </summary>
        public string Header { get; set; } = null!;

        /// <summary> Текст поста </summary>
        public string Body { get; set; } = null!;
    }
}
