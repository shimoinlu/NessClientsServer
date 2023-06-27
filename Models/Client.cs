using System.ComponentModel.DataAnnotations;

namespace NessClientsServer.Models;

public class Client
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string SureName { get; set; }
   
    public required string PersonalId { get; set; }    
    public required string IpAddress { get; set; }    
    public required string PhonoNumber { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
}