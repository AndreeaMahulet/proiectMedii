using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
namespace project.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly CompanyContext _context;
        private string _baseUrl = "http://localhost:49856/api/Departments";

        public DepartmentsController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var departments = JsonConvert.DeserializeObject<List<Department>>(await
               response.Content.
                ReadAsStringAsync());
                return View(departments);
            }
            return NotFound();
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var department = JsonConvert.DeserializeObject<Department>(
                await response.Content.ReadAsStringAsync());
                return View(department);
            }
            return NotFound();
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentID,Name")] Department department)
        {
            if (!ModelState.IsValid) return View(department);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(department);
                var response = await client.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: { ex.Message} ");
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var department = JsonConvert.DeserializeObject<Department>(
                await response.Content.ReadAsStringAsync());
                return View(department);
            }
            return new NotFoundResult();

        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentID,Name")] Department department)
        {
            if (!ModelState.IsValid) return View(department);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(department);
            var response = await client.PutAsync($"{_baseUrl}/{department.DepartmentID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var department = JsonConvert.DeserializeObject<Department>(await
               response.Content.ReadAsStringAsync());
                return View(department);
            }
            return new NotFoundResult();
        }
        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("DepartmentID")] Department department)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete,
               $"{_baseUrl}/{department.DepartmentID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(department),
               Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record:{ ex.Message} ");
            }
            return View(department);
        }
        // POST: Departments/Delete/5
        /* [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             var department = await _context.Departments.FindAsync(id);
             _context.Departments.Remove(department);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool DepartmentExists(int id)
         {
             return _context.Departments.Any(e => e.DepartmentID == id);
         }*/
    }
}
