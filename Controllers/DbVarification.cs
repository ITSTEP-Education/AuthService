using AuthJWTAspNetWeb.Database;
using System.Text.RegularExpressions;


namespace _20240723_SqlDb_Gai.Controllers
{
    public static class DbVarification
    {
        private static readonly string patternNumber = @"^[A-Z]{2}\d{4}[A-Z]{2}$";
        public static bool IsDbContext(AuthDbContext carContext) => carContext.Database.CanConnect();

        public static bool IsDbCars(AuthDbContext carContext) => carContext.Cars != null ? true : false;

        public static bool isNumber(string number) => Regex.IsMatch(number, patternNumber, RegexOptions.IgnoreCase);

        //public static StatusCode isSaveToDb(AuthDbContext carContext, string msg = "db saved")
        //{
        //    try
        //    {
        //        carContext.SaveChanges();
        //        return new StatusCode201(msg);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new StatusCode400($"{ex.InnerException!.Message.ToString()}");
        //    }
        //}
    }
}
