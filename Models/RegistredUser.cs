using System.ComponentModel.DataAnnotations;

namespace Examine.Models;

public class RegistredUser
{
    [MaxLength(50)]
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public DateTime DateOfBirthday { get; set; }
    
    [MaxLength(25)]
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }
    [MaxLength(50)]
    [Required]
    public string Presentation { get; set; }
    [Required]
    [Key]
    [MaxLength(6)]
    public string Key { get; set; }
}