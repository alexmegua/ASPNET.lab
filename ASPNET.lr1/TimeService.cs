namespace MyCalcApp.Services
{
    public class TimeService
    {
        public string GetTimeOfDayMessage()
        {
            var now = DateTime.Now.TimeOfDay;

            if (now.Hours >= 6 && now.Hours < 12)
            {
                return "Зараз ранок";
            }
            else if (now.Hours >= 12 && now.Hours < 18)
            {
                return "Зараз день";
            }
            else if (now.Hours >= 18 && now.Hours < 24)
            {
                return "Зараз вечір";
            }
            else
            {
                return "Зараз ніч";
            }
        }
    }
}
