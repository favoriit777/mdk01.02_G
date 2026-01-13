using System;
using System.Linq;

namespace StringCalculatorLib
{
    /// <summary>
    /// Класс Calculator с методами для работы со строками и числами
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Возвращает перевернутую строку
        /// </summary>
        /// <param name="input">Исходная строка</param>
        /// <returns>Перевернутая строка</returns>
        public string ReverseString(string input)
        {
            if (input == null)
                return null;

            // Преобразуем строку в массив символов, переворачиваем и создаем новую строку
            return new string(input.Reverse().ToArray());
        }

        /// <summary>
        /// Дополнительный метод: Проверяет, является ли строка палиндромом
        /// </summary>

        

        /// <summary>
        /// Дополнительный метод: Возвращает длину строки
        /// </summary>
        public int GetStringLength(string input)
        {
            return input?.Length ?? 0;
        }

        /// <summary>
        /// Дополнительный метод: Проверяет, пустая ли строка или null
        /// </summary>
        public bool IsStringNullOrEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Дополнительный метод: Конвертирует строку в верхний регистр
        /// </summary>
        public string ToUpperCase(string input)
        {
            return input?.ToUpper() ?? null;
        }

        public bool IsPalindrome(string input)
        {
            throw new NotImplementedException();
        }
    }
}