using ContactManager.Common.Model;
using ContactManager.Models;
using ContactManager.Repository.Entities;
using ContactManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {

        private readonly IContactService _contactService;
        private readonly IConfiguration _configuration;

        public ContactController( IContactService contactService,IConfiguration configuration)
        {
         
            this._contactService = contactService;
            this._configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var contactModel = await _contactService.GetAll();
            var googleMapAPi = _configuration.GetValue<string>("Setting:GoogleMapAPI");
            ViewBag.GoogleMapAPI = googleMapAPi;

            return View(contactModel);
        }
        public async Task<IActionResult> Details(int id)
        {

            var contact = await _contactService.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Json(contact);


        }

        public async Task<IActionResult> Create()
        {
            var googleMapAPi = _configuration.GetValue<string>("Setting:GoogleMapAPI");
            ViewBag.GoogleMapAPI = googleMapAPi;
            return View();
        }
   
        [HttpPost]
        public async Task<IActionResult> Create(ContactDTO contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactService.AddOrUpdateService(contact);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Validation failed", errors });
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _contactService.DeleteContact(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

   
    }
}
