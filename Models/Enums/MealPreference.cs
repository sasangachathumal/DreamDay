using System.ComponentModel.DataAnnotations;

public enum MealPreference
{
    [Display(Name = "Non-Vegetarian")]
    NonVegetarian,
    [Display(Name = "Vegetarian")]
    Vegetarian,
    [Display(Name = "Vegan")]
    Vegan
}