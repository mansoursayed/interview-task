using System.ComponentModel.DataAnnotations;

namespace InterviewTask.Application.Contract.Driver;
public class UpdateDriverDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
    [MaxLength(50)]
    public string PhoneNumber { get; set; }
}