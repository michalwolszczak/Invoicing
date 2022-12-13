using InvoicingWebCore.Data;
using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace InvoicingWebCore.Controllers
{
    [Authorize]
    public class ContractorController : Controller
    {
        private readonly IContractorService _contractorService;
        private readonly ILogger _logger;

        public ContractorController(IContractorService contractorService, ILogger<ContractorController> logger)
        {
            _contractorService = contractorService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var contractors = _contractorService.GetAll();

                return View(contractors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error has occurred. Please try again");
                throw;
            }            
        }

        //get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                if (_contractorService.Create(contractor))
                {
                    TempData["success"] = "A new contractor has been added";
                    return RedirectToAction("Index");
                }
            }

            TempData["error"] = "Something went wrong";
            _logger.LogError("Model is invalid", ModelState.Values.Select(x => x.Errors));

            return View(contractor);
        }

        //get
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var contractor = _contractorService.Get(id);

            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contractor model)
        {
            if (ModelState.IsValid)
            {
                _contractorService.Update(model);

                TempData["success"] = "Contractor has been updated";
                return RedirectToAction("Index");
            }

            _logger.LogError("Model is invalid", ModelState.Values.Select(x => x.Errors));
            return View(model);
        }

        //get
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }

                if (_contractorService.Delete(id))
                {
                    TempData["success"] = "Contractor has been deleted";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error has occurred");
                throw;
            }
        }

        [HttpPost]
        public JsonResult Get(int contractorId)
        {
            try
            {
                return Json(_contractorService.Get(contractorId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error has occurred");
                throw;
            }            
        }
    }
}
