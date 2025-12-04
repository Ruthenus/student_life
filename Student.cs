using System;
using System.Linq;  // Average()

namespace student_life
{
    public class Student
    {
        // Особисті дані
        private string surname = string.Empty;     // Прізвище
        private string name = string.Empty;        // Ім'я
        private string patronymic = string.Empty;  // По батькові
        private DateTime dateOfBirth;   // Дата народження
        // https://learn.microsoft.com/ru-ru/dotnet/api/system.datetime?view=net-8.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.datetime?view=net-8.0
        private string? homeAddress;    // Домашня адреса
        private string? phoneNumber;    // Номер телефону

        // Академічна інформація
        private int[]? courseGrades;     // Оцінки за курсами
        private string[]? courseTitles;  // Назви курсів
        private bool[]? examPassed;      // Іспити (true — здано)


        // Приватні методи встановлення значень приватних полів
        private void SetSurname(string value) { surname = value ?? 
                string.Empty; }
        private void SetName(string value) { name = value ?? string.Empty; }
        private void SetPatronymic(string value) { patronymic = value ?? 
                string.Empty; }
        private void SetDateOfBirth(DateTime value) { dateOfBirth = value; }
        private void SetHomeAddress(string? value) { homeAddress = value; }
        private void SetPhoneNumber(string? value) { phoneNumber = value; }
        private void SetCourseGrades(int[]? value) { courseGrades = value; }
        private void SetCourseTitles(string[]? value) { courseTitles = value; }
        private void SetExamPassed(bool[]? value) { examPassed = value; }


        // Конструктор без параметрів
        public Student()
        {
            SetSurname("КАЧУРОВСЬКИЙ");
            SetName("ФЕДІР");
            SetPatronymic("СТЕПАНОВИЧ");
            SetDateOfBirth(new DateTime(1933, 4, 2));
            SetHomeAddress("Ген. Петрова 57/29");
            SetPhoneNumber("64-57-82");
        }


        // Конструктор з 3 параметрами (делегування)
        public Student(string name, string patronymic, DateTime dateOfBirth)
            : this()
        {
            SetName(name);
            SetPatronymic(patronymic);
            SetDateOfBirth(dateOfBirth);
        }


        // Повний конструктор для класу Student
        public Student(string? surname, string? name, string? patronymic, 
            DateTime? dateOfBirth, string? homeAddress, string? phoneNumber,
            int[]? courseGrades, string[]? courseTitles, bool[]? examPassed)
            {
                SetSurname(surname ?? string.Empty);
                SetName(name ?? string.Empty);
                SetPatronymic(patronymic ?? string.Empty);
                SetDateOfBirth(dateOfBirth ?? DateTime.UnixEpoch);
                SetHomeAddress(homeAddress);
                SetPhoneNumber(phoneNumber);
                SetCourseGrades(courseGrades);
                SetCourseTitles(courseTitles);
                SetExamPassed(examPassed);

                // Перевірка узгодженості довжин масивів з академічною
                // інформацією: кількість оцінок, назв курсів та результатів
                // іспитів має бути однаковою
                if (courseGrades?.Length != courseTitles?.Length || 
                    courseGrades?.Length != examPassed?.Length)
                    throw new ArgumentException("Масиви даних про курси " +
                        "повинні мати однакову довжину.");
            }


        // Методи одержання значень приватних полів
        public string GetSurname() { return surname; }
        public string GetName() { return name; }
        public string GetPatronymic() { return patronymic; }
        public DateTime GetDateOfBirth() { return dateOfBirth; }
        public string? GetHomeAddress() { return homeAddress; }
        public string? GetPhoneNumber() { return phoneNumber; }
        public int[]? GetCourseGrades() { return courseGrades; }
        public string[]? GetCourseTitles() { return courseTitles; }
        public bool[]? GetExamPassed() { return examPassed; }


        // Властивості
        public string Name  // ім'я
        {
            get { return name; }
            set { SetName(value); }
        }

        public string LastName  // прізвище
        {
            get { return surname; }
            set { SetSurname(value); }
        }

        public int Age  // вік
        {
            get  // read-only властивість
            {
                var today = DateTime.Today;
                int age = today.Year - dateOfBirth.Year;
                // Якщо день народження ще не настав цього року —
                // зменшуємо вік на 1 рік
                if (today < dateOfBirth.AddYears(age)) age--;
                return age;
            }
        }

        public double AverageGrade  // середній бал
        {
            get
            {
                if (courseGrades == null || courseGrades.Length == 0) 
                    return 0.0;
                // Використовуємо LINQ-метод для обчислення середнього значення
                return courseGrades.Average();
                // https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.average?view=net-10.0
            }
        }


        // Перевантаження операторів
        public static bool operator ==(Student? a, Student? b)
        {
            if (a is null || b is null) return false;
            return Math.Abs(a.AverageGrade - b.AverageGrade) < 0.001;
            // https://learn.microsoft.com/en-us/dotnet/api/system.math?view=net-10.0
        }

        public static bool operator !=(Student? a, Student? b)
        { 
            return !(a == b); 
        }

        public static bool operator >(Student? a, Student? b)
        {
            if (a is null) return false;
            if (b is null) return false;
            return a.AverageGrade > b.AverageGrade;
        }

        public static bool operator <(Student? a, Student? b)
        {
            return b > a;
        }

        // CS0660 "Student" определяет оператор "==" или оператор "!=",
        // но не переопределяет Object.Equals(object o).
        // CS0661 "Student" определяет оператор "==" или оператор "!=",
        // но не переопределяет Object.GetHashCode().

        // Для коректності порівняння перевизначаємо Equals з класу object
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type
        public override bool Equals(object? someStranger)
        {
            // Перевіряємо, чи переданий об'єкт взагалі є Student.
            // Використовуємо pattern matching: якщо someStranger
            // не Student або null, то повертаємо false
            if (someStranger is not Student otherStudent)
            {
                Console.WriteLine("Це не студент або посилання є null");
                return false;
            }
            // Якщо це Student, то порівнюємо середні бали (з допуском)
            return Math.Abs(otherStudent.AverageGrade - this.AverageGrade) < 
                0.001;
        }

        // Для коректної роботи в колекціях разом із Equals обов'язково
        // перевизначаємо GetHashCode.
        // https://learn.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=net-10.0
        public override int GetHashCode()
        {
            return AverageGrade.GetHashCode();
            // Тут використовуємо хеш-код від AverageGrade, щоб
            // узгодити його з логікою Equals.
        }


        // Метод показу всіх даних про студента
        public void DisplayInfo()
        {
            Console.WriteLine("\tІНФОРМАЦІЯ ПРО СТУДЕНТА");
            Console.WriteLine($"ПІБ: {surname} {name} {patronymic}");
            Console.WriteLine($"Дата народження: {dateOfBirth:dd.MM.yyyy}");
            Console.WriteLine($"Домашня адреса: {homeAddress}");
            Console.WriteLine($"Номер телефону: {phoneNumber}");

            Console.WriteLine("\n\tАкадемічна інформація");
            if (courseTitles != null && courseGrades != null && 
                examPassed != null)
            {
                for (int i = 0; i < courseTitles.Length; i++)
                {
                    string status = examPassed[i] ? "Здано" : "Не здано";
                    Console.WriteLine($"Курс: {courseTitles[i]}");
                    Console.WriteLine($"Оцінка: {courseGrades[i]}");
                    Console.WriteLine($"Іспит: {status}");
                }
            }
            else
            {
                Console.WriteLine("Академічна інформація відсутня.");
            }
            Console.WriteLine($"Середній бал: {AverageGrade:F2}");
        }
    }
}