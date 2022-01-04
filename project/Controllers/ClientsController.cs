using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;
using project.Models.LibraryViewModels;

namespace project.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
   // [Authorize(Policy = "ManagerPolicy")]
    public class ClientsController : Controller
    {
        private readonly CompanyContext _context;

        public ClientsController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(int? id, int? bookID)
        {
            var viewModel = new ClientIndexData();
            viewModel.Clients = await _context.Clients
            .Include(i => i.EmplyeesForClient)
            .ThenInclude(i => i.Employee)
            .ThenInclude(i => i.Managers)
            .ThenInclude(i => i.Department)
            .AsNoTracking()
            .OrderBy(i => i.ClientName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["ClientID"] = id.Value;
                Client client = viewModel.Clients.Where(
                i => i.ID == id.Value).Single();
                viewModel.Employees = client.EmplyeesForClient.Select(s => s.Employee);
            }
            if (bookID != null)
            {
                ViewData["EmployeeID"] = id.Value;
                viewModel.Managers = viewModel.Employees.Where(
                x => x.ID == id).Single().Managers;
            }
            return View(viewModel);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == id);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClientName,Adress")] Client client)
        {
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
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
           .Include(i => i.EmplyeesForClient).ThenInclude(i => i.Employee)
 .AsNoTracking()
 .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }
            PopulateEmployeeForClientData(client);
            return View(client);

        }
        private void PopulateEmployeeForClientData(Client client)
        {
            var allEmployees = _context.Employees;
            var employeeForClients = new HashSet<int>(client.EmplyeesForClient.Select(c => c.EmployeeID));
            var viewModel = new List<EmployeeForClientData>();
            foreach (var employee in allEmployees)
            {
                viewModel.Add(new EmployeeForClientData
                {
                    EmployeeID = employee.ID,
                    Name = employee.Name,
                    IsWorkingForClient = employeeForClients.Contains(employee.ID)
                });
            }
            ViewData["Employees"] = viewModel;
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedEmployees)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clientToUpdate = await _context.Clients
            .Include(i => i.EmplyeesForClient)
            .ThenInclude(i => i.Employee)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Client>(
            clientToUpdate,
            "",
            i => i.ClientName, i => i.Adress))
            {
                UpdateEmployeeForClient(selectedEmployees, clientToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateEmployeeForClient(selectedEmployees, clientToUpdate);
            PopulateEmployeeForClientData(clientToUpdate);
            return View(clientToUpdate);
        }
        private void UpdateEmployeeForClient(string[] selectedEmployees, Client clientToUpdate)
        {
            if (selectedEmployees == null)
            {
                clientToUpdate.EmplyeesForClient = new List<EmplyeesForClient>();
                return;
            }
            var selectedEmployeesHS = new HashSet<string>(selectedEmployees);
            var employeesForClient = new HashSet<int>
            (clientToUpdate.EmplyeesForClient.Select(c => c.Employee.ID));
            foreach (var employee in _context.Employees)
            {
                if (selectedEmployeesHS.Contains(employee.ID.ToString()))
                {
                    if (!employeesForClient.Contains(employee.ID))
                    {
                        clientToUpdate.EmplyeesForClient.Add(new EmplyeesForClient
                        {
                            ClientID =
                       clientToUpdate.ID,
                            EmployeeID = employee.ID
                        });
                    }
                }
                else
                {
                    if (employeesForClient.Contains(employee.ID))
                    {
                        EmplyeesForClient employeeToRemove = clientToUpdate.EmplyeesForClient.FirstOrDefault(i
                       => i.EmployeeID == employee.ID);
                        _context.Remove(employeeToRemove);
                    }
                }
            }
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
    }
}
