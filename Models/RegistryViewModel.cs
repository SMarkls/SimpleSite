using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Examine.Models;

public class RegistryViewModel
{
    [MaxLength(50)]
    [Required]
    [BindProperty(Name = "last_name")]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [MaxLength(25)]
    [Required]  
    public string Phone { get; set; }
    
    [Required]
    public string Presentation { get; set; }
}