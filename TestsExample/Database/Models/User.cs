namespace TestsExample.Database.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary> Идентификатор пользователя </summary>
        public Guid Id { get; set; }

        /// <summary> Имя пользователя </summary>
        public string Name { get; set; } = null!;

        /// <summary> Пароль пользователя </summary>
        public string Password { get; set; } = null!;
    }
}
