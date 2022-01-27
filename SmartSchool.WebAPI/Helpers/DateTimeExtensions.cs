using System;

namespace SmartSchool.WebAPI.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime datetime)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - datetime.Year;

            if (currentDate < datetime.AddYears(age))
                age--;

            return age;
        }
    }
}