using System.ComponentModel.DataAnnotations;

public class OldAddress
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string Number { get; set; }

    [Required]
    public string Postcode { get; set; }

    [Required]
    public string City { get; set; }
}
