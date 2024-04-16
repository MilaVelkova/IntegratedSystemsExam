using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using IntegratedSystems.Domain.DTO;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IVaccinationCenterService _vaccinationCenterService;

        public VaccinationCentersController(IPatientService patientService, IVaccinationCenterService vaccinationCenterService)
        {
            _patientService = patientService;
            _vaccinationCenterService = vaccinationCenterService;
        }

        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(_vaccinationCenterService.GetVaccinationCenter());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterId(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                vaccinationCenter.Id = Guid.NewGuid();
                _vaccinationCenterService.CreateNewVaccinayonCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterId(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vaccinationCenterService.UpdateVaccinationCenter(vaccinationCenter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationCenterExists(vaccinationCenter.Id))
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
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterId(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterId(id);
            if (vaccinationCenter != null)
            {
                _vaccinationCenterService.DeleteVaccinationCenter(id);
            }

           
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ScheduleVaccination(Guid id)
        {
            var vaccCenter = _vaccinationCenterService.GetVaccinationCenterId(id);
            if (vaccCenter.MaxCapacity <= 0)
            {
                return RedirectToAction(nameof(NoMoreCapacity));
            }
            VaccinationDTO dto = new VaccinationDTO();
            dto.vaccCenterId = id;
            dto.patients = _patientService.GetPatients();
            dto.manufacturers = new List<string>()
            {
                "Fajzer", "Sputnik", "Astra Zeneca"
            };
            return View(dto);
        }

        [HttpPost, ActionName("Schedule")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmSchedule(VaccinationDTO dto)
        {
            if (ModelState.IsValid)
            {
                var vaccCenter = _vaccinationCenterService.GetVaccinationCenterId(dto.vaccCenterId);
                vaccCenter.MaxCapacity--;
                _vaccinationCenterService.UpdateVaccinationCenter(vaccCenter);
                _vaccinationCenterService.ScheduleVaccine(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public IActionResult NoMoreCapacity()
        {
            return View();
        }
        private bool VaccinationCenterExists(Guid id)
        {
            return _vaccinationCenterService.GetVaccinationCenterId(id) != null;
        }
    }
}
