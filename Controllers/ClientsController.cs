using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NessClientsServer.Data;
using NessClientsServer.Models;

namespace NessClientsServer.Controllers
{
    public class ClientsController : Controller
    {
        private readonly NessClientsServerContext _context;

        public ClientsController(NessClientsServerContext context)
        {
            _context = context;
        }

        //GET: Clients
        public async Task<IActionResult> Index()
        {
              return _context.Client != null ? 
                          View(await _context.Client.ToListAsync()) :
                          Problem("Entity set 'NessClientsServerContext.Client'  is null.");
        }
        //GET: Clients/EditScreen
        public async Task<IActionResult> EditScreen()
        {
            ViewData["allowEdit"] = "true";
            return _context.Client != null ? 
                          View("Index",await _context.Client.ToListAsync()) :
                          Problem("Entity set 'NessClientsServerContext.Client'  is null.");
        }
        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SureName,PersonalId,IpAddress,PhonoNumber")] Client client)
        {
            string urlToIpInformation = "http://ip-api.com/json/" + client.IpAddress;
            HttpClient c = new HttpClient();
            HttpResponseMessage response = await c.GetAsync(urlToIpInformation);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonNode ipNode = JsonNode.Parse(responseBody)!;
            string city = (string)ipNode!["city"]!;
            string country = (string)ipNode!["country"]!;

            client.City = city;
            client.Country = country;

            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
       }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SureName,PersonalId,IpAddress,PhonoNumber,Country,City")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'NessClientsServerContext.Client'  is null.");
            }
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Search(string? FirstName,string? SureName, string? PersonalId)
        {
            if (_context.Client== null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            } 
            var clnts = from c in _context.Client
                select c;

            if (!String.IsNullOrEmpty(FirstName))
            {
                clnts = clnts.Where(s => s.FirstName!.Contains(FirstName));
            }
            if (!String.IsNullOrEmpty(SureName))
            {
                clnts = clnts.Where(s => s.FirstName!.Contains(SureName));
            }
            if (!String.IsNullOrEmpty(PersonalId))
            {
                clnts = clnts.Where(s => s.FirstName!.Contains(PersonalId));
            }
            ViewData["SearchScreen"] = "yes";
            if(String.IsNullOrEmpty(PersonalId) && String.IsNullOrEmpty(FirstName) && String.IsNullOrEmpty(SureName) )
                return View("index",await _context.Client.ToListAsync());
            else
                return View("index",await clnts.ToListAsync());
        }
        private bool ClientExists(int id)
        {
          return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
