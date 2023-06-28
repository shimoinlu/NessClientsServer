using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NessClientsServer.Models;

public class Client
{
    public int Id { get; set; }
    [Display(Name = "First name")]
    [Required(ErrorMessage ="you must init your first name")]
    [RegularExpression(@"^[A-Za-z ]+$",ErrorMessage ="you can init only letters and spaces")]
    [StringLength(maximumLength:20,ErrorMessage ="you can init only 20 characters")]
    public required string FirstName { get; set; }

    [Display(Name = "Sure name")]
    [Required(ErrorMessage ="you must init your sure name")]
    [RegularExpression(@"^[A-Za-z ]+$",ErrorMessage ="you can init only letters and spaces")]
    [StringLength(maximumLength:20,ErrorMessage ="you can init only 20 characters")]
    public required string SureName { get; set; }
   
    [Display(Name = "Id")]
    [Required(ErrorMessage ="you must init id")]
    [RegularExpression(@"^\d+$",ErrorMessage ="you can init only digits")]
    [StringLength(9,MinimumLength =9,ErrorMessage ="you can init exact 9 digits")]
    public required string PersonalId { get; set; }    
 
    [Display(Name = "Ip address")]
    [Required(ErrorMessage ="you must init your ip address")]
    [RegularExpression(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$",ErrorMessage ="you must init a valid ip address")]
    public required string IpAddress { get; set; }    
    [Display(Name = "Phone number")]
    [Required(ErrorMessage ="you must init your phone number")]
    [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){9,}$",ErrorMessage ="you must init a valid phone number")]
    public required string PhonoNumber { get; set; }
    [Required]
    [RegularExpression(@"[a-zA-Z \-]+")]
    public required string Country { get; set; }
    [Required]
    [RegularExpression(@"[a-zA-Z \-]+")]
    public required string City { get; set; }
}