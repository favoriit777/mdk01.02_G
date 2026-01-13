using Xunit;

namespace ExceptionTestingDemo.Tests
{
    public class StudentRegistryTests
    {
        [Fact]
        public void AddStudent_ExistingId_ThrowsInvalidOperationException()
        {
            var registry = new StudentRegistry();
            var student = new Student(1, "John");

            registry.AddStudent(student);

            var duplicateStudent = new Student(1, "Jane");

            var ex = Assert.Throws<InvalidOperationException>(() => registry.AddStudent(duplicateStudent));
            Assert.Contains("already exists", ex.Message);
        }

        [Fact]
        public void GetStudent_NonExistentId_ThrowsStudentNotFoundException()
        {
            var registry = new StudentRegistry();

            var ex = Assert.Throws<StudentNotFoundException>(() => registry.GetStudent(999));
            Assert.Equal(999, ex.StudentId);
            Assert.Contains("not found", ex.Message);
        }

        [Fact]
        public void UpdateGrade_NonExistentStudent_ThrowsStudentNotFoundException()
        {
            var registry = new StudentRegistry();

            var ex = Assert.Throws<StudentNotFoundException>(() => registry.UpdateGrade(123, "Math", 85));
            Assert.Equal(123, ex.StudentId);
        }

        [Fact]
        public void UpdateGrade_InvalidGrade_ThrowsArgumentOutOfRangeException()
        {
            var registry = new StudentRegistry();
            var student = new Student(1, "John");
            registry.AddStudent(student);

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => registry.UpdateGrade(1, "Math", -5));
            Assert.Equal("grade", ex.ParamName);
        }

        [Fact]
        public void AddStudent_NullStudent_ThrowsArgumentNullException()
        {
            var registry = new StudentRegistry();
            Assert.Throws<ArgumentNullException>(() => registry.AddStudent(null));
        }
    }
}