namespace PriceRater.API.Helpers
{
    public static class DateTimeHelpers
    {
        public static TimeSpan TimeBetweenTwoDates(DateTime start, DateTime end)
        {
            return start - end; 
        }
    }
}