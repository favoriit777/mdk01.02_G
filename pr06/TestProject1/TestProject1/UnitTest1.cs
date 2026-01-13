using Xunit;
using ParametrizedTestsDemo;
using System;
using System.Collections.Generic;

namespace ParametrizedTestsDemo.Tests
{
    public class GeometryCalculatorTests
    {
        private readonly GeometryCalculator _calculator;

        public GeometryCalculatorTests()
        {
            _calculator = new GeometryCalculator();
        }

        // ==================== ТЕСТЫ С ИСПОЛЬЗОВАНИЕМ [InlineData] ====================

        [Theory]
        [InlineData(3, 4, 5, 6)]               // Прямоугольный треугольник
        [InlineData(5, 5, 6, 12)]              // Равнобедренный треугольник
        [InlineData(6, 8, 10, 24)]             // Другой прямоугольный треугольник
        [InlineData(7, 8, 9, 26.832815729997)] // Остроугольный треугольник
        public void CalculateTriangleArea_ValidTriangles_ReturnsCorrectArea(double a, double b, double c, double expected)
        {
            // Act
            double result = _calculator.CalculateTriangleArea(a, b, c);

            // Assert
            Assert.Equal(expected, result, 10); // Точность до 10 знаков
        }

        [Theory]
        [InlineData(1, 0, 1)]          // Нулевая сторона
        [InlineData(-1, 2, 2)]         // Отрицательная сторона
        [InlineData(1, 1, 3)]          // Несуществующий треугольник (1+1=2 < 3)
        [InlineData(1, 2, 1)]          // Вырожденный треугольник
        public void CalculateTriangleArea_InvalidTriangles_ThrowsException(double a, double b, double c)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.CalculateTriangleArea(a, b, c));
        }

        [Theory]
        [InlineData(3, 4, 5, true)]    // Классический прямоугольный треугольник
        [InlineData(5, 12, 13, true)]  // Еще один прямоугольный треугольник
        [InlineData(6, 8, 10, true)]   // Увеличенный 3-4-5 треугольник
        [InlineData(2, 3, 4, false)]   // Не прямоугольный треугольник
        [InlineData(5, 5, 5, false)]   // Равносторонний треугольник (не прямоугольный)
        public void IsRightTriangle_VariousTriangles_ReturnsCorrectResult(double a, double b, double c, bool expected)
        {
            // Act
            bool result = _calculator.IsRightTriangle(a, b, c);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1.0, 2 * Math.PI)]         // Радиус 1
        [InlineData(2.0, 4 * Math.PI)]         // Радиус 2
        [InlineData(5.0, 10 * Math.PI)]        // Радиус 5
        [InlineData(10.0, 20 * Math.PI)]       // Радиус 10
        [InlineData(0.5, Math.PI)]             // Радиус 0.5
        public void CalculateCircleCircumference_ValidRadius_ReturnsCorrectValue(double radius, double expected)
        {
            // Act
            double result = _calculator.CalculateCircleCircumference(radius);

            // Assert
            Assert.Equal(expected, result, 10);
        }

        [Theory]
        [InlineData(0)]     // Нулевой радиус
        [InlineData(-1)]    // Отрицательный радиус
        [InlineData(-5.5)]  // Отрицательный дробный радиус
        public void CalculateCircleCircumference_InvalidRadius_ThrowsException(double radius)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.CalculateCircleCircumference(radius));
        }

        // ==================== ТЕСТЫ С ИСПОЛЬЗОВАНИЕМ [MemberData] ====================

        public static IEnumerable<object[]> TriangleAreaTestData
        {
            get
            {
                // Правильные треугольники
                yield return new object[] { 3.0, 4.0, 5.0, 6.0 };
                yield return new object[] { 13.0, 14.0, 15.0, 84.0 };
                yield return new object[] { 7.0, 8.0, 9.0, 26.832815729997478 };
                yield return new object[] { 11.0, 12.0, 13.0, 61.48170459575759 };

                // Равносторонние треугольники
                yield return new object[] { 1.0, 1.0, 1.0, 0.4330127018922193 };
                yield return new object[] { 2.0, 2.0, 2.0, 1.7320508075688772 };
                yield return new object[] { 5.0, 5.0, 5.0, 10.825317547305483 };

                // Практические примеры
                yield return new object[] { 8.0, 15.0, 17.0, 60.0 };
                yield return new object[] { 9.0, 12.0, 15.0, 54.0 };
            }
        }

        [Theory]
        [MemberData(nameof(TriangleAreaTestData))]
        public void CalculateTriangleArea_MemberData_ReturnsCorrectArea(double a, double b, double c, double expected)
        {
            // Act
            double result = _calculator.CalculateTriangleArea(a, b, c);

            // Assert
            Assert.Equal(expected, result, 10);
        }

        public static IEnumerable<object[]> RightTriangleTestData
        {
            get
            {
                // Пифагоровы тройки
                yield return new object[] { 3.0, 4.0, 5.0, true };
                yield return new object[] { 5.0, 12.0, 13.0, true };
                yield return new object[] { 8.0, 15.0, 17.0, true };
                yield return new object[] { 7.0, 24.0, 25.0, true };
                yield return new object[] { 20.0, 21.0, 29.0, true };

                // Не прямоугольные треугольники
                yield return new object[] { 2.0, 3.0, 4.0, false };
                yield return new object[] { 5.0, 5.0, 8.0, false };
                yield return new object[] { 6.0, 7.0, 8.0, false };

                // Пограничные случаи (почти прямоугольные)
                yield return new object[] { 3.0, 4.0, 5.0000001, false };
                yield return new object[] { 3.0, 4.0, 4.9999999, false };
            }
        }

        [Theory]
        [MemberData(nameof(RightTriangleTestData))]
        public void IsRightTriangle_MemberData_ReturnsCorrectResult(double a, double b, double c, bool expected)
        {
            // Act
            bool result = _calculator.IsRightTriangle(a, b, c);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> CircleCircumferenceTestData
        {
            get
            {
                // Целые радиусы
                yield return new object[] { 1.0, 2 * Math.PI };
                yield return new object[] { 2.0, 4 * Math.PI };
                yield return new object[] { 3.0, 6 * Math.PI };
                yield return new object[] { 10.0, 20 * Math.PI };

                // Дробные радиусы
                yield return new object[] { 0.5, Math.PI };
                yield return new object[] { 1.5, 3 * Math.PI };
                yield return new object[] { 2.5, 5 * Math.PI };

                // Большие радиусы
                yield return new object[] { 100.0, 200 * Math.PI };
                yield return new object[] { 1000.0, 2000 * Math.PI };
            }
        }

        [Theory]
        [MemberData(nameof(CircleCircumferenceTestData))]
        public void CalculateCircleCircumference_MemberData_ReturnsCorrectValue(double radius, double expected)
        {
            // Act
            double result = _calculator.CalculateCircleCircumference(radius);

            // Assert
            Assert.Equal(expected, result, 10);
        }

        // ==================== ТЕСТЫ ДЛЯ ДОПОЛНИТЕЛЬНЫХ МЕТОДОВ ====================

        [Theory]
        [InlineData(5, 5, 5, true)]    // Равносторонний
        [InlineData(3, 3, 3, true)]    // Равносторонний
        [InlineData(3, 4, 5, false)]   // Не равносторонний
        [InlineData(5, 5, 6, false)]   // Равнобедренный, но не равносторонний
        public void IsEquilateralTriangle_VariousTriangles_ReturnsCorrectResult(double a, double b, double c, bool expected)
        {
            // Act
            bool result = _calculator.IsEquilateralTriangle(a, b, c);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 5, 6, true)]    // Равнобедренный
        [InlineData(6, 5, 5, true)]    // Равнобедренный (стороны в другом порядке)
        [InlineData(5, 6, 5, true)]    // Равнобедренный
        [InlineData(3, 4, 5, false)]   // Разносторонний
        [InlineData(5, 5, 5, true)]    // Равносторонний тоже считается равнобедренным
        public void IsIsoscelesTriangle_VariousTriangles_ReturnsCorrectResult(double a, double b, double c, bool expected)
        {
            // Act
            bool result = _calculator.IsIsoscelesTriangle(a, b, c);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3, 4, 5, 12)]      // Прямоугольный треугольник
        [InlineData(5, 5, 5, 15)]      // Равносторонний треугольник
        [InlineData(7, 8, 9, 24)]      // Остроугольный треугольник
        public void CalculateTrianglePerimeter_ValidTriangles_ReturnsCorrectValue(double a, double b, double c, double expected)
        {
            // Act
            double result = _calculator.CalculateTrianglePerimeter(a, b, c);

            // Assert
            Assert.Equal(expected, result);
        }
    }

    // ==================== ТЕСТЫ С ИСПОЛЬЗОВАНИЕМ [ClassData] ====================

    public class TriangleAreaComplexTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Различные типы треугольников с точными значениями площади

            // Прямоугольные треугольники
            yield return new object[] { 3.0, 4.0, 5.0, 6.0 };
            yield return new object[] { 6.0, 8.0, 10.0, 24.0 };
            yield return new object[] { 5.0, 12.0, 13.0, 30.0 };

            // Равносторонние треугольники
            yield return new object[] { 2.0, 2.0, 2.0, Math.Sqrt(3) };
            yield return new object[] { 4.0, 4.0, 4.0, 4 * Math.Sqrt(3) };

            // Практические примеры из реальных задач
            yield return new object[] { 7.0, 24.0, 25.0, 84.0 };
            yield return new object[] { 8.0, 15.0, 17.0, 60.0 };
            yield return new object[] { 9.0, 40.0, 41.0, 180.0 };

            // Треугольники с дробными сторонами
            yield return new object[] { 2.5, 3.5, 4.5, 4.353070037341462 };
            yield return new object[] { 1.2, 1.3, 1.4, 0.704163145007595 };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class RightTriangleComplexTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Известные пифагоровы тройки
            yield return new object[] { 3, 4, 5, true };
            yield return new object[] { 5, 12, 13, true };
            yield return new object[] { 8, 15, 17, true };
            yield return new object[] { 7, 24, 25, true };
            yield return new object[] { 20, 21, 29, true };
            yield return new object[] { 12, 35, 37, true };
            yield return new object[] { 9, 40, 41, true };

            // Не прямоугольные треугольники
            yield return new object[] { 4, 5, 6, false };
            yield return new object[] { 7, 8, 9, false };
            yield return new object[] { 10, 10, 15, false };

            // Порядок сторон не важен
            yield return new object[] { 5, 3, 4, true };
            yield return new object[] { 13, 5, 12, true };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class CircleCircumferenceComplexTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Различные радиусы с вычисленными значениями
            yield return new object[] { 0.1, 0.2 * Math.PI };
            yield return new object[] { 0.25, 0.5 * Math.PI };
            yield return new object[] { 0.5, Math.PI };
            yield return new object[] { 0.75, 1.5 * Math.PI };
            yield return new object[] { 1.0, 2.0 * Math.PI };
            yield return new object[] { 2.5, 5.0 * Math.PI };
            yield return new object[] { 5.0, 10.0 * Math.PI };
            yield return new object[] { 7.5, 15.0 * Math.PI };
            yield return new object[] { 10.0, 20.0 * Math.PI };
            yield return new object[] { 12.5, 25.0 * Math.PI };
            yield return new object[] { 15.0, 30.0 * Math.PI };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class InvalidTriangleTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Все возможные невалидные случаи
            yield return new object[] { 0.0, 4.0, 5.0 };      // Нулевая сторона
            yield return new object[] { 3.0, 0.0, 5.0 };      // Нулевая сторона
            yield return new object[] { 3.0, 4.0, 0.0 };      // Нулевая сторона

            yield return new object[] { -1.0, 4.0, 5.0 };     // Отрицательная сторона
            yield return new object[] { 3.0, -4.0, 5.0 };     // Отрицательная сторона
            yield return new object[] { 3.0, 4.0, -5.0 };     // Отрицательная сторона

            yield return new object[] { 1.0, 1.0, 2.0 };      // Вырожденный треугольник
            yield return new object[] { 1.0, 2.0, 3.0 };      // Вырожденный треугольник

            yield return new object[] { 1.0, 1.0, 3.0 };      // Несуществующий треугольник
            yield return new object[] { 2.0, 3.0, 6.0 };      // Несуществующий треугольник
            yield return new object[] { 5.0, 1.0, 1.0 };      // Несуществующий треугольник

            yield return new object[] { 0.0, 0.0, 0.0 };      // Все стороны нулевые
            yield return new object[] { -1.0, -1.0, -1.0 };   // Все стороны отрицательные
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    // Классы тестов с использованием ClassData

    public class TriangleAreaClassDataTests
    {
        private readonly GeometryCalculator _calculator = new GeometryCalculator();

        [Theory]
        [ClassData(typeof(TriangleAreaComplexTestData))]
        
    }

    public class RightTriangleClassDataTests
    {
        private readonly GeometryCalculator _calculator = new GeometryCalculator();

        [Theory]
        [ClassData(typeof(RightTriangleComplexTestData))]
        public void IsRightTriangle_ClassData_ReturnsCorrectResult(double a, double b, double c, bool expected)
        {
            // Act
            bool result = _calculator.IsRightTriangle(a, b, c);

            // Assert
            Assert.Equal(expected, result);
        }
    }

    public class CircleCircumferenceClassDataTests
    {
        private readonly GeometryCalculator _calculator = new GeometryCalculator();

        [Theory]
        [ClassData(typeof(CircleCircumferenceComplexTestData))]
        public void CalculateCircleCircumference_ClassData_ReturnsCorrectValue(double radius, double expected)
        {
            // Act
            double result = _calculator.CalculateCircleCircumference(radius);

            // Assert
            Assert.Equal(expected, result, 10);
        }
    }

    public class InvalidTriangleClassDataTests
    {
        private readonly GeometryCalculator _calculator = new GeometryCalculator();

        [Theory]
        [ClassData(typeof(InvalidTriangleTestData))]
        public void CalculateTriangleArea_InvalidTriangles_ClassData_ThrowsException(double a, double b, double c)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.CalculateTriangleArea(a, b, c));
        }

        [Theory]
        [ClassData(typeof(InvalidTriangleTestData))]
        public void IsRightTriangle_InvalidTriangles_ClassData_ThrowsException(double a, double b, double c)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.IsRightTriangle(a, b, c));
        }
    }
}