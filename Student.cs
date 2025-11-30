using System;

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
        }
    }
}