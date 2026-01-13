using Xunit;
using StringCalculatorLib;

namespace StringCalculatorLib.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        public CalculatorTests()
        {
            // Arrange - создаем экземпляр калькулятора для всех тестов
            _calculator = new Calculator();
        }

        // ==================== ТЕСТЫ ДЛЯ ReverseString ====================

        [Theory]
        [InlineData("hello", "olleh")]
        [InlineData("world", "dlrow")]
        [InlineData("12345", "54321")]
        public void ReverseString_ValidString_ReturnsReversedString(string input, string expected)
        {
            // Act
            string result = _calculator.ReverseString(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReverseString_EmptyString_ReturnsEmptyString()
        {
            // Arrange
            string input = "";
            string expected = "";

            // Act
            string result = _calculator.ReverseString(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReverseString_NullString_ReturnsNull()
        {
            // Arrange
            string input = null;

            // Act
            string result = _calculator.ReverseString(input);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("AB", "BA")]
        public void ReverseString_SingleOrTwoCharacters_ReturnsCorrectResult(string input, string expected)
        {
            // Act
            string result = _calculator.ReverseString(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("hello world", "dlrow olleh")]
        [InlineData("C# .NET", "TEN. #C")]
        [InlineData("тест", "тсет")]
        public void ReverseString_StringWithSpacesAndSpecialChars_ReturnsReversedString(string input, string expected)
        {
            // Act
            string result = _calculator.ReverseString(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReverseString_LongString_ReturnsReversedString()
        {
            // Arrange
            string input = new string('a', 1000);
            string expected = new string('a', 1000); // Для строки из одинаковых символов перевернутая будет такой же

            // Act
            string result = _calculator.ReverseString(input);

            // Assert
            Assert.Equal(expected, result);
        }

        // ==================== ТЕСТЫ ДЛЯ IsPalindrome ====================

        [Theory]
        [InlineData("hello", 5)]
        [InlineData("", 0)]
        [InlineData("12345", 5)]
        public void GetStringLength_ValidString_ReturnsCorrectLength(string input, int expected)
        {
            // Act
            int result = _calculator.GetStringLength(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetStringLength_NullString_ReturnsZero()
        {
            // Arrange
            string input = null;
            int expected = 0;

            // Act
            int result = _calculator.GetStringLength(input);

            // Assert
            Assert.Equal(expected, result);
        }

        // ==================== ТЕСТЫ ДЛЯ IsStringNullOrEmpty ====================

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData(" ", false)]
        [InlineData("hello", false)]
        public void IsStringNullOrEmpty_VariousInputs_ReturnsCorrectResult(string input, bool expected)
        {
            // Act
            bool result = _calculator.IsStringNullOrEmpty(input);

            // Assert
            Assert.Equal(expected, result);
        }

        // ==================== ТЕСТЫ ДЛЯ ToUpperCase ====================

        [Theory]
        [InlineData("hello", "HELLO")]
        [InlineData("Hello World", "HELLO WORLD")]
        [InlineData("123", "123")]
        public void ToUpperCase_ValidString_ReturnsUpperCaseString(string input, string expected)
        {
            // Act
            string result = _calculator.ToUpperCase(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ToUpperCase_NullOrEmptyString_ReturnsSameValue(string input)
        {
            // Act
            string result = _calculator.ToUpperCase(input);

            // Assert
            Assert.Equal(input, result);
        }

        // ==================== ДОПОЛНИТЕЛЬНЫЕ ТЕСТЫ ====================

        [Fact]
        public void ReverseString_ThenReverseAgain_ReturnsOriginalString()
        {
            // Arrange
            string original = "Test String";

            // Act
            string reversed = _calculator.ReverseString(original);
            string reversedAgain = _calculator.ReverseString(reversed);

            // Assert
            Assert.Equal(original, reversedAgain);
        }

        [Theory]
        [InlineData("madam")]
        [InlineData("Madam")]
        public void ReverseString_ForPalindrome_ReturnsSameString(string input)
        {
            // Act
            string reversed = _calculator.ReverseString(input);

            // Assert
            // Для палиндрома без учета регистра
            Assert.Equal(input, reversed, ignoreCase: true);
        }
    }
}