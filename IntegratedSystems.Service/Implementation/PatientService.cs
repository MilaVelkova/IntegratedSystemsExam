using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientrepository;

        public PatientService(IRepository<Patient> patientrepository)
        {
            _patientrepository = patientrepository;
        }

        public Patient CreateNewPatient(Patient patient)
        {
           return _patientrepository.Insert(patient);
        }

        public Patient DeletePatient(Guid? id)
        {
            var patient = _patientrepository.Get(id);
            return _patientrepository.Delete(patient);
        }

        public Patient GetPatientId(Guid? id)
        {
            return _patientrepository.Get(id);
        }

        public List<Patient> GetPatients()
        {
            return _patientrepository.GetAll().ToList();
        }

        public Patient UpdatePatient(Patient patient)
        {
            return _patientrepository.Update(patient);
        }
    }
}
