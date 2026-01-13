public class StudentRegistry
{
    private readonly Dictionary<int, Student> _students = new Dictionary<int, Student>();

    public void AddStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));
        if (_students.ContainsKey(student.Id))
            throw new InvalidOperationException($"Student with ID {student.Id} already exists.");
        _students.Add(student.Id, student);
    }

    public Student GetStudent(int id)
    {
        if (!_students.TryGetValue(id, out var student))
            throw new StudentNotFoundException(id);
        return student;
    }

    public void UpdateGrade(int studentId, string subject, int grade)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("Subject cannot be null or empty.", nameof(subject));
        if (grade < 0 || grade > 100)
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100.");
        var student = GetStudent(studentId);
        student.Grades[subject] = grade;
    }
}
public class StudentNotFoundException : Exception
{
    public int StudentId { get; }

    public StudentNotFoundException(int studentId)
        : base($"Student with ID {studentId} was not found.")
    {
        StudentId = studentId;
    }
}
public class Student
{
    public int Id { get; }
    public string Name { get; set; }
    public Dictionary<string, int> Grades { get; }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
        Grades = new Dictionary<string, int>();
    }
}