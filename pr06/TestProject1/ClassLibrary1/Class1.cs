using System;

namespace ParametrizedTestsDemo
{
    /// <summary>
    /// Класс для геометрических расчетов
    /// </summary>
    public class GeometryCalculator
    {
        /// <summary>
        /// Вычисляет площадь треугольника по трем сторонам (формула Герона)
        /// </summary>
        /// <param name="a">Сторона A</param>
        /// <param name="b">Сторона B</param>
        /// <param name="c">Сторона C</param>
        /// <returns>Площадь треугольника</returns>
        public double CalculateTriangleArea(double a, double b, double c)
        {
            // Проверка на валидность треугольника
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentException("Все стороны треугольника должны быть положительными");

            if (a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException("Треугольник с такими сторонами не существует");

            // Формула Герона
            double p = (a + b + c) / 2;
            double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            // Проверка на вырожденность (площадь близка к нулю)
            if (area < 1e-10)
                throw new ArgumentException("Треугольник вырожденный");

            return area;
        }

        /// <summary>
        /// Проверяет, является ли треугольник прямоугольным
        /// </summary>
        public bool IsRightTriangle(double a, double b, double c)
        {
            // Проверка на валидность треугольника
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentException("Все стороны треугольника должны быть положительными");

            if (a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException("Треугольник с такими сторонами не существует");

            // Сортируем стороны для удобства проверки
            double[] sides = { a, b, c };
            Array.Sort(sides);

            // Проверяем теорему Пифагора с допуском на погрешность вычислений
            double tolerance = 1e-10;
            double hypotenuseSquared = Math.Pow(sides[2], 2);
            double legsSquaredSum = Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2);

            return Math.Abs(hypotenuseSquared - legsSquaredSum) < tolerance;
        }

        /// <summary>
        /// Вычисляет длину окружности
        /// </summary>
        /// <param name="radius">Радиус окружности</param>
        /// <returns>Длина окружности</returns>
        public double CalculateCircleCircumference(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Радиус должен быть положительным");

            return 2 * Math.PI * radius;
        }

        /// <summary>
        /// Дополнительный метод: Вычисляет площадь круга
        /// </summary>
        public double CalculateCircleArea(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Радиус должен быть положительным");

            return Math.PI * radius * radius;
        }

        /// <summary>
        /// Дополнительный метод: Проверяет, является ли треугольник равносторонним
        /// </summary>
        public bool IsEquilateralTriangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                return false;

            double tolerance = 1e-10;
            return Math.Abs(a - b) < tolerance && Math.Abs(b - c) < tolerance;
        }

        /// <summary>
        /// Дополнительный метод: Проверяет, является ли треугольник равнобедренным
        /// </summary>
        public bool IsIsoscelesTriangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                return false;

            if (a + b <= c || a + c <= b || b + c <= a)
                return false;

            double tolerance = 1e-10;
            return Math.Abs(a - b) < tolerance || Math.Abs(a - c) < tolerance || Math.Abs(b - c) < tolerance;
        }

        /// <summary>
        /// Дополнительный метод: Находит периметр треугольника
        /// </summary>
        public double CalculateTrianglePerimeter(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentException("Все стороны треугольника должны быть положительными");

            if (a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException("Треугольник с такими сторонами не существует");

            return a + b + c;
        }
    }
}