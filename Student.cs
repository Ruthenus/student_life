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

        // Конструктор без параметрів
        internal Student()
        {
            surname = "КАЧУРОВСЬКИЙ";
            name = "ФЕДІР";
            patronymic = "СТЕПАНОВИЧ";
            dateOfBirth = new DateTime(1933, 4, 2);
            homeAddress = "Ген. Петрова 57/29";
            phoneNumber = "64-57-82";
        }

        // Конструктор з 3 параметрами (делегування)
        internal Student(string name, string patronymic, DateTime dateOfBirth)
            : this()
        { 
            this.name = name;
            this.patronymic = patronymic;
            this.dateOfBirth = dateOfBirth;
        }

        // Повний конструктор для класу Student
        public Student(string? surname, string? name, string? patronymic, 
            DateTime? dateOfBirth, string? homeAddress, string? phoneNumber,
            int[]? courseGrades, string[]? courseTitles, bool[]? examPassed)
            {
                this.surname = surname ?? string.Empty;
                this.name = name ?? string.Empty;
                this.patronymic = patronymic ?? string.Empty;
                this.dateOfBirth = dateOfBirth ?? DateTime.UnixEpoch;
                this.homeAddress = homeAddress ?? string.Empty;
                this.phoneNumber = phoneNumber ?? string.Empty;
                this.courseGrades = courseGrades;
                this.courseTitles = courseTitles;
                this.examPassed = examPassed;

                // Перевірка узгодженості довжин масивів з академічною
                // інформацією: кількість оцінок, назв курсів та результатів
                // іспитів має бути однаковою
                if (courseGrades?.Length != courseTitles?.Length || 
                    courseGrades?.Length != examPassed?.Length)
                    throw new ArgumentException("Масиви даних про курси " +
                        "повинні мати однакову довжину.");
            }
        

        // Гетери та сетери в стилі С++

        // Ім'я
        public void SetName(string value) { name = value; }
        public string GetName() { return name; }

        // По батькові
        public void SetPatronymic(string value) { patronymic = value; }
        public string GetPatronymic() { return patronymic; }

        // Прізвище
        public void SetSurname(string value) { surname = value; }
        public string GetSurname() { return surname; }

        // Дата народження
        public void SetDateOfBirth(DateTime value) { dateOfBirth = value; }
        public DateTime GetDateOfBirth() { return dateOfBirth; }

        // Домашня адреса
        public void SetHomeAddress(string? value) { homeAddress = value; }
        public string? GetHomeAddress() { return homeAddress; }

        // Номери телефонів
        public void SetPhoneNumber(string? value) { phoneNumber = value; }
        public string? GetPhoneNumber() { return phoneNumber; }

        // Оцінки за курсами
        public void SetCourseGrades(int[]? value) { courseGrades = value; }
        public int[]? GetCourseGrades() { return courseGrades; }

        // Назви курсів
        public void SetCourseTitles(string[]? value) { courseTitles = value; }
        public string[]? GetCourseTitles() { return courseTitles; }

        // Іспити
        public void SetExamPassed(bool[]? value) { examPassed = value; }
        public bool[]? GetExamPassed() { return examPassed; }

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