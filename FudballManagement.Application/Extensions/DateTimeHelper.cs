namespace FudballManagement.Application.Extensions
{
    public static class DateTimeHelper
    {
        public static DateTime GetUtcPlus5Now()
        {
            return DateTime.UtcNow.AddHours(5);
        }
    }

}
