using ContactManager.Common.Model;
using ContactManager.Models;
using ContactManager.Repository.Entities;
using ContactManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;

        public ContactController(ILogger<ContactController> logger, IContactService contactService)
        {
            _logger = logger;
            this._contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var contactModel = await _contactService.GetAll();
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

        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var contact = await _contactService.GetById(id.Value);
                if (contact == null)
                {
                    return NotFound();
                }
                return View(contact);
            }

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

            // If model state is not valid, return errors
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
