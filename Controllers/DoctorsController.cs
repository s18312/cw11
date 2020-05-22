using cw11.DTOs.Requests;
using cw11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {

        private readonly CodeFirstContext _context;
        public IConfiguration Configuration { get; set; }

        public DoctorsController(CodeFirstContext context, IConfiguration configuration)
        {
            Configuration = configuration;
            _context = context;
        }


        [HttpGet]
        public IActionResult GetDoctors()
        {
            var list = _context.Doctor.Select(doctor => new { IdDoctor = doctor.IdDoctor, FirstName = doctor.FirstName, LastName = doctor.LastName, Email = doctor.Email });
            return Ok(list);
        }

        [HttpPut]
        public IActionResult UpdateDoctor(ChangeRequest doc)
        {
            var doctor = _context.Doctor.Where(dc => dc.IdDoctor == doc.IdDoctor);

            Doctor changed = new Doctor { IdDoctor = doc.IdDoctor, FirstName = doc.FirstName, LastName = doc.LastName };
            _context.Attach(changed);
            _context.Entry(changed).Property("FirstName").IsModified = true;
            _context.Entry(changed).Property("LastName").IsModified = true;
            _context.Entry(changed).Property("Email").IsModified = true;
            _context.SaveChanges();

            return Ok("Zaktualizowano doktora o numerze:" + changed.IdDoctor);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _context.Doctor.Where(doc => doc.IdDoctor == id);
            _context.Remove(doctor);
            _context.SaveChanges();

            return Ok("Usunięto doktora o numerze: " + id);
        }

        [HttpPost]
        public IActionResult CreateDoctor(AddDoctorRequest addDoctor)
        {
            Doctor doctor = new Doctor();
            //var maxId = _context.Doctor.Max(doc => doc.IdDoctor);
            //doctor.IdDoctor = maxId + 1;
            doctor.FirstName = addDoctor.FirstName;
            doctor.LastName = addDoctor.LastName;
            doctor.Email = addDoctor.Email;
            _context.Doctor.Add(doctor);
            _context.SaveChanges();
            return Ok(doctor);
        }

    }
}
