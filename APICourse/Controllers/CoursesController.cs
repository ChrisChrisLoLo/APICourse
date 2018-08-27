using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APICourse.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICourse.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly APICourseContext _context;

        public CoursesController(APICourseContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Course>> GetAll()
        {
            return _context.Courses.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "value";
        }

        // GET single
        [HttpGet("{subjectCode}_{courseCode}")]
        public string GetByCode(string subjectCode,int courseCode)
        {
            
            return subjectCode+courseCode.ToString();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
