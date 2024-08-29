using System.ComponentModel.DataAnnotations;

namespace AuthJWTAspNetWeb.Models
{
    public class Car
    {
        public int id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Neccessary registration number")]
        [RegularExpression(@"^[a-zA-Z]{2}\d{4}[a-zA-Z]{2}$", ErrorMessage = "uncorrect format")]
        public string number { get; set; }
        public string? vinCode { get; set; }
        public string model { get; set; }

        [Range(0.01, 6.0, ErrorMessage = "Available value from 0.01 to 6.0")]
        public float volume { get; set; }
        public double price { get; set; }

        public Car(string? number, string vinCode, string? model, float volume, double price)
        {
            this.number = number!.ToUpper();
            this.vinCode = vinCode.ToUpper();
            this.model = model!.ToUpper();
            this.volume = volume;
            this.price = price;
        }
    }
}
