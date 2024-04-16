using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IRepository<VaccinationCenter> _vaccCenterRepository;
        private readonly IRepository<Vaccine> _vaccinerepository;

        public VaccinationCenterService (IRepository<VaccinationCenter> VaccCenterrepository, IRepository<Vaccine> vaccinerepository)
        {
            _vaccCenterRepository = VaccCenterrepository;
            _vaccinerepository = vaccinerepository;
        }

        public VaccinationCenter CreateNewVaccinayonCenter(VaccinationCenter vaccinationCenter)
        {
            return _vaccCenterRepository.Insert(vaccinationCenter);
        }

        public VaccinationCenter DeleteVaccinationCenter(Guid? id)
        {
            var center = _vaccCenterRepository.Get(id);
            return _vaccCenterRepository.Delete(center);
        }

        public List<VaccinationCenter> GetVaccinationCenter()
        {
            return _vaccCenterRepository.GetAll().ToList();
        }

        public VaccinationCenter GetVaccinationCenterId(Guid? id)
        {
            return _vaccCenterRepository.Get(id);
        }

        public void ScheduleVaccine(VaccinationDTO dto)
        {
            Vaccine vaccine = new Vaccine();
            vaccine.Manufacturer = dto.manufacturer;
            vaccine.Certificate = Guid.NewGuid();
            vaccine.VaccinationCenter = dto.vaccCenterId;
            vaccine.PatientId = dto.patientId;
            vaccine.DateTaken = dto.vaccinationDate;
            vaccine.Center = _vaccCenterRepository.Get(dto.vaccCenterId);
            _vaccinerepository.Insert(vaccine);

        }

        public VaccinationCenter UpdateVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            return _vaccCenterRepository.Update(vaccinationCenter);
        }
    }
}
