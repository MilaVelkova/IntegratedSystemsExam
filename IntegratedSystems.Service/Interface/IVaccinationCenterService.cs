using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccinationCenterService
    {
        List<VaccinationCenter> GetVaccinationCenter();
        public VaccinationCenter GetVaccinationCenterId(Guid? id);
        public VaccinationCenter CreateNewVaccinayonCenter(VaccinationCenter vaccinationCenter);
        public VaccinationCenter UpdateVaccinationCenter(VaccinationCenter vaccinationCenter);
        public VaccinationCenter DeleteVaccinationCenter(Guid? id);

        public void ScheduleVaccine(VaccinationDTO dto);
    }
}
