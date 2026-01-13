using System;

namespace AssertMethodsDemo
{
    /// <summary>
    /// Класс для работы с датами и временем
    /// </summary>
    public class DateHelper
    {
        /// <summary>
        /// Проверяет, является ли год високосным
        /// </summary>
        /// <param name="year">Год для проверки</param>
        /// <returns>True если год високосный, иначе False</returns>
        public bool IsLeapYear(int year)
        {
            if (year < 1 || year > 9999)
                throw new ArgumentOutOfRangeException(nameof(year), "Year must be between 1 and 9999");

            // Правило високосного года:
            // Год високосный, если он делится на 4, но не делится на 100,
            // либо делится на 400
            return (year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0);
        }

        /// <summary>
        /// Возвращает количество дней в месяце с учетом года
        /// </summary>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц (1-12)</param>
        /// <returns>Количество дней в месяце</returns>
        public int GetDaysInMonth(int year, int month)
        {
            if (year < 1 || year > 9999)
                throw new ArgumentOutOfRangeException(nameof(year), "Year must be between 1 and 9999");

            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12");

            // Массив дней в месяцах для обычного года
            int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // Для февраля проверяем високосный год
            if (month == 2 && IsLeapYear(year))
                return 29;

            return daysInMonth[month - 1];
        }

        /// <summary>
        /// Проверяет, является ли дата выходным днем
        /// </summary>
        /// <param name="date">Дата для проверки</param>
        /// <returns>True если дата - суббота или воскресенье, иначе False</returns>
        public bool IsWeekend(DateTime date)
        {
            // DayOfWeek.Sunday = 0, DayOfWeek.Saturday = 6
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Дополнительный метод: Получает название дня недели
        /// </summary>
        public string GetDayOfWeekName(DateTime date)
        {
            return date.DayOfWeek.ToString();
        }

        /// <summary>
        /// Дополнительный метод: Проверяет, является ли дата сегодняшним днем
        /// </summary>
        public bool IsToday(DateTime date)
        {
            return date.Date == DateTime.Today;
        }

        /// <summary>
        /// Дополнительный метод: Вычисляет возраст по дате рождения
        /// </summary>
        public int CalculateAge(DateTime birthDate)
        {
            if (birthDate > DateTime.Today)
                throw new ArgumentException("Birth date cannot be in the future", nameof(birthDate));

            int age = DateTime.Today.Year - birthDate.Year;

            // Если день рождения еще не наступил в этом году, вычитаем 1 год
            if (birthDate.Date > DateTime.Today.AddYears(-age))
                age--;

            return age;
        }
    }
}