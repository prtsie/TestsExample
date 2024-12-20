﻿using System.ComponentModel.DataAnnotations;
using TestsExample.Models;

namespace Layers.Application.Requests;

/// <summary>
/// Запрос на создание нового поста
/// </summary>
public class CreatePostRequest
{
    /// <summary> Заголовок поста </summary>
    [Required(ErrorMessage = "Это поле обязательно")]
    [StringLength(
        Constants.MaxPostTitleLength,
        ErrorMessage = "Длина должна быть не больше {1} символов")]
    public string Title { get; set; } = null!;

    /// <summary> Текст поста </summary>
    [Required(ErrorMessage = "Это поле обязательно")]
    [StringLength(
        Constants.MaxPostTextLength,
        ErrorMessage = "Длина должна быть не больше {1} символов")]
    public string Content { get; set; } = null!;
}