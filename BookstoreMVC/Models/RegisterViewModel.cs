using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Imię")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Nick")]
    public string Nick { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Powtórz hasło")]
    [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
    public string ConfirmPassword { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "Musisz zaakceptować regulamin.")]
    public bool AcceptTerms { get; set; }
}
