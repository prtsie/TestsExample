using System.ComponentModel.DataAnnotations;

namespace Layers.Application.Models;

public enum Sort
{
    [Display(Name = "Сначала новые")]
    Date,
    [Display(Name = "Сначала старые")]
    DateReverse,
    [Display(Name = "По автору")]
    Author,
    [Display(Name = "По автору с конца")]
    AuthorReverse,
    [Display(Name = "По названию")]
    Title,
    [Display(Name = "По названию с конца")]
    TitleReverse
}