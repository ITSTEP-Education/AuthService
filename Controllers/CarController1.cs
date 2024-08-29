using AuthJWTAspNetWeb.Database;
using AuthJWTAspNetWeb.Models;
using AuthJWTAspNetWeb.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _20240723_SqlDb_Gai.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public partial class CarController : ControllerBase
    {
        private readonly AuthDbContext dbContext;
        private readonly ILogger<CarController> logger;

        public CarController(ILogger<CarController> logger, AuthDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        private Car? getCar(string number) => this.dbContext.Cars.FirstOrDefault(car => car.number!.Equals(number.ToUpper()));

        [HttpGet(Name = "GetCars")]
        public ActionResult<IEnumerable<Car>> Get() {
            if (!DbVarification.IsDbCars(this.dbContext))
                return StatusCode(StatusCodes.Status404NotFound, new Response() { Status = "Error", Message = "no db records for cars" });

            return Ok(this.dbContext.Cars);
        }

        [HttpGet("Number/{number}", Name = "GetByNumber")]
        public ActionResult<Car> Get([Required] string number) {
            //if (!DbVarification.isNumber(number)) 
            //    return StatusCode(StatusCodes.Status400BadRequest, new Response() { Status = "Error", Message = $"uncorrect format {number}" });
            if (!DbVarification.IsDbCars(this.dbContext)) 
                return StatusCode(StatusCodes.Status404NotFound, new Response() { Status = "Error", Message = "no db records for cars" });

            Car? car = this.dbContext.Cars.FirstOrDefault(car => car.number!.Equals(number.ToUpper()));

            return  car != null ? Ok(car) : StatusCode(StatusCodes.Status404NotFound, new Response() { Status = "Error", Message = $"{number} is absent in db" });
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete(Name = "DeleteCarId")]
        public IActionResult Delete([Required] string number) {

            //if (!DbVarification.isNumber(number)) 
            //    return StatusCode(StatusCodes.Status400BadRequest, new Response() { Status = "Error", Message = $"uncorrect format {number}" });
            if (!DbVarification.IsDbCars(this.dbContext)) 
                return StatusCode(StatusCodes.Status404NotFound, new Response() { Status = "Error", Message = "no db records for cars" });

            Car? car = this.dbContext.Cars.FirstOrDefault(x => x.number.Equals(number.ToUpper()));

            if (car == null) {
                return StatusCode(StatusCodes.Status404NotFound, new Response() { Status = "Error", Message = $"{number} is absent in db" });
            }

            this.dbContext.Cars.Remove(car!);
            return Ok(DbVarification.isSaveToDb(this.dbContext, $"{number} entity is deleted from db"));
        }
    }
}
