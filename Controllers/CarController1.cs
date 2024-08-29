using AuthJWTAspNetWeb.Database;
using AuthJWTAspNetWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _20240723_SqlDb_Gai.Controllers
{
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

        /// <summary>
        /// Get &lt;Cars> from db
        /// </summary>
        /// <returns></returns>
        /// <responce code="200">Successful request fulfillment</responce>
        /// <responce code="404">Failed request: No records in table cars of SqlDb carsdata</responce>
        /// <responce code="409">Failed request: No connection to SqlDb carsdata</responce>

        [HttpGet(Name = "GetCars")]
        [ProducesResponseType(typeof(IEnumerable<Car>), 200)]
        public ActionResult<IEnumerable<Car>> Get() {
            if (!DbVarification.IsDbCars(this.dbContext))
                return StatusCode(StatusCodes.Status404NotFound, new Response() { Status = "Error", Message = "no db records for cars" });

            return Ok(this.dbContext.Cars);
        }

        /// <summary>
        /// Get instance of Car by its number
        /// </summary>
        /// <param name="Number">Registration Number: AE4000IT</param>
        /// <returns></returns>
        /// <responce code="200">Successful request fulfillment</responce>
        /// <responce code="400">Failed request: Uncorrect format of number inputed</responce>
        /// <responce code="404">Failed request: No records in table cars of SqlDb carsdata</responce>
        /// <responce code="409">Failed request: No connection to SqlDb carsdata</responce>
        /// 
        [HttpGet("Number/{Number}", Name = "GetByNumber")]
        [ProducesResponseType(typeof(Car), 200)]
        public ActionResult<Car> Get([Required] string Number) {
            if (!DbVarification.isNumber(Number)) return BadRequest(new StatusCode400($"uncorrect format {Number}"));
            else if (!DbVarification.IsDbCars(this.dbContext)) return NotFound(new StatusCode404());

            Car? car = this.dbContext.Cars.FirstOrDefault(car => car.number!.Equals(Number.ToUpper()));

            //IEnumerable<Car> cars = (from car in _carContext.Cars.Include(c => c._Mark).Include(c => c._Color)
            //            where car.Number.Equals(Number.ToUpper())
            //            select new Car(car, car._Mark, car._Color));

            return  car != null ? Ok(car) : BadRequest(new StatusCode400( $"{Number} model is absent in db"));
        }

        /// <summary>
        /// Delete from db entity Car by registration Number
        /// </summary>
        /// <param name="number">Registration Number: AE4000IT</param>
        /// <returns></returns>
        /// <responce code="200">Successful request fulfillment</responce>
        /// <responce code="400">Failed request: Uncorrect format of number inputed</responce>
        /// <responce code="404">Failed request: Not found data</responce>
        /// <responce code="409">Failed request: No connection to SqlDb carsdata</responce>
        [HttpDelete(Name = "DeleteCarId")]
        public IActionResult Delete([Required] string number) {

            if (!DbVarification.isNumber(number)) return BadRequest(new StatusCode400($"uncorrect format {number}"));
            else if (!DbVarification.IsDbCars(this.dbContext)) return NotFound(new StatusCode404());

            Car? car = this.dbContext.Cars.FirstOrDefault(x => x.number.Equals(number.ToUpper()));
            if (car == null) {
                return NotFound(new StatusCode404($"{number} is absent entity in db"));
            }

            this.dbContext.Cars.Remove(car!);
            //return Ok(DbVarification.isSaveToDb(this.dbContext, $"{number} entity is deleted from db"));
            return Ok();
        }
    }
}
