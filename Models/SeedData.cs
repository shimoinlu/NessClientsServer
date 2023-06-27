using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NessClientsServer.Data;
using System;
using System.Linq;

namespace NessClientsServer.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new NessClientsServerContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<NessClientsServerContext>>()))
        {
            // Look for any Clients.
            if (context.Client.Any())
            {
                return;   // DB has been seeded
            }
            context.Client.AddRange(
                new Client
                {
                    FirstName = "Priscilla",
                    SureName = "Matthews",
                    PersonalId = "384525101",
                    IpAddress = "80.119.117.187",
                    PhonoNumber = "+972-523862672",
                    Country = "France",
                    City = "Villiers-sur-Marne"
                },
                new Client
                {
                    FirstName = "Benjamin",
                    SureName = "Douglas",
                    PersonalId = "660652470",
                    IpAddress = "104.29.98.222",
                    PhonoNumber = "+972-557712987",
                    Country = "Saudi Arabia",
                    City = "Riyadh"                
                }
            );
            context.SaveChanges();
        }
    }
}