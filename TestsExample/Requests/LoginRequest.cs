using System.ComponentModel.DataAnnotations;
using TestsExample.Database.Models;

namespace TestsExample.Requests;

public class LoginRequest
{
    /// <summary> <inheritdoc cref="User.Name" /> </summary>
    [Required(ErrorMessage = "Это поле обязательно")]
    [StringLength(
        Constants.MaxUsernameLength,
        MinimumLength = Constants.MinUsernameLength,
        ErrorMessage = "Длина должна быть от {2} до {1} символов")]
    public string Name { get; set; } = null!;

    /// <summary> <inheritdoc cref="User.Password" /> </summary>
    [Required(ErrorMessage = "Это поле обязательно")]
    [StringLength(
        Constants.MaxPasswordLength,
        MinimumLength = Constants.MinPasswordLength,
        ErrorMessage = "Длина должна быть от {2} до {1} символов")]
    public string Password { get; set; } = null!;
}