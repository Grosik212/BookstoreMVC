using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public string Publisher { get; set; }

    [Required]
    public int Year { get; set; }

    [Required]
    public int Pages { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Img { get; set; }
}
