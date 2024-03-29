﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


public class ApplicationUser : IdentityUser
{

}

public class Address
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string Number { get; set; }

    [Required]
    public string Postcode { get; set; }

    [Required]
    public string City { get; set; }
}