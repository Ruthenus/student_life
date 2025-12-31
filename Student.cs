using System;
using System.Linq;  // Average()

namespace student_life
{
    using SchArgs = ScholarshipAwardedEventArgs;  // alias

    // Клас для передачі даних у події ScholarshipAwarded
    public class ScholarshipAwardedEventArgs(double averageGrade) : EventArgs
    {
        public double AverageGrade { get; } = averageGrade;
        // Додаткова інформація про стипендію — фіксована сума 1000 грн.
        public decimal ScholarshipAmount { get; } = 1000m;
    }


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
        private void SetSurname(string value)
        {
            surname = value ??
                string.Empty;
        }
        private void SetName(string value) { name = value ?? string.Empty; }
        private void SetPatronymic(string value)
        {
            patronymic = value ??
                string.Empty;
        }
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
            SetCourseGrades([12, 12, 12, 12, 12, 12]);
            SetExamPassed([true, true, true, true, true, true]);
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

            CheckTime();
            CheckForAutomat();
            CheckScholarship();
        }


        // Вкладений клас-компаратор, який порівнює 2 студентів за
        // зростанням середнього балу (ascending). У разі рівних балів —
        // за алфавітом ПІБ.
        public class AverageGradeComparer : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            // Єдиний метод інтерфейсу IComparer<T>
            // Повертає:
            //   < 0  — x йде перед y
            //   = 0  — x і y вважаються рівними
            //   > 0  — x йде після y
            // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icomparer-1.compare
            {
                // Захист від null — за контрактом IComparer<T> аргументи
                // не повинні бути null.
                if (x == null)
                    throw new ArgumentNullException(nameof(x),
                        "Надати нормального студента, а не працюючого!");
                if (y == null)
                    throw new ArgumentNullException(nameof(y),
                        "Надати нормального студента, а не працюючого!");

                // Порівнюємо за середнім балом (зростання).
                // CompareTo поверне -1, 0 або 1 — саме те, що потрібно!
                int gradeComparison = x.AverageGrade.CompareTo(y.AverageGrade);
                // Якщо бали різні — повертаємо результат порівняння балів
                if (gradeComparison != 0) return gradeComparison;

                // Якщо бали однакові — порівнюємо за сформованим ПІБ
                // (алфавітно, нечутливо до регістру)
                string fullNameX = $"{x.GetSurname()} {x.GetName()} " +
                    $"{x.GetPatronymic()}".Trim();
                string fullNameY = $"{y.GetSurname()} {y.GetName()} " +
                    $"{y.GetPatronymic()}".Trim();
                return string.Compare(fullNameX, fullNameY,
                    StringComparison.OrdinalIgnoreCase);
            }
        }


        // Вкладений клас-компаратор, який порівнює 2 студентів за ПІБ 
        // в алфавітному порядку (case-insensitive). У разі однакового
        // ПІБ — за середнім балом (descending).
        public class FullNameComparer : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            {
                // Аналогічний захист від null
                if (x == null)
                    throw new ArgumentNullException(nameof(x),
                        "Надати нормального студента, а не працюючого!");
                if (y == null)
                    throw new ArgumentNullException(nameof(y),
                        "Надати нормального студента, а не працюючого!");

                // Формуємо повне ПІБ для порівняння
                string fullNameX = $"{x.GetSurname()} {x.GetName()} " +
                    $"{x.GetPatronymic()}".Trim();
                string fullNameY = $"{y.GetSurname()} {y.GetName()} " +
                    $"{y.GetPatronymic()}".Trim();

                // Порівнюємо ПІБ (алфавітно, нечутливо до регістру)
                int nameComparison = string.Compare(fullNameX, fullNameY,
                    StringComparison.OrdinalIgnoreCase);
                // Якщо ПІБ різні — повертаємо результат порівняння імен
                if (nameComparison != 0) return nameComparison;

                // Якщо ПІБ однакові — сортуємо за середнім балом у спадному
                // порядку. Тому порівнюємо y з x (щоб кращий бал йшов першим).
                return y.AverageGrade.CompareTo(x.AverageGrade);
            }
        }


        // Подія LectureMissed, що використовує власний делегат та
        // спрацьовує, якщо при виклику методу CheckTime поточний системний
        // час перевищує час завчасного прибуття на заняття о 08:55. 
        public delegate void LectureMissedHandler(object sender, EventArgs e);
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
        public event LectureMissedHandler? LectureMissed;
        //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/

        // Метод, що перевіряє час і, за потреби, генерує подію LectureMissed
        public void CheckTime()
        {
            var now = DateTime.Now;
            var lectureStart = new DateTime(now.Year, now.Month, now.Day,
                8, 55, 0);

            if (now > lectureStart)
            {
                // Генерація події: сповіщаємо всіх підписників після 08:55.
                // sender — це поточний об'єкт Student, e — порожні дані
                // (EventArgs.Empty)
                LectureMissed?.Invoke(this, EventArgs.Empty);
            }
        }


        // Стандартна подія AutomatReceived з вбудованим делегатом
        // EventHandler (не генерує додаткових даних).
        // Викликається при автоматичній високій оцінці (всі оцінки == 12).
        public event EventHandler? AutomatReceived;
        // https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler?view=net-10.0

        // Метод перевірки на "автомат" і генерація відповідної події
        public void CheckForAutomat()
        {
            int[]? grades = GetCourseGrades();
            // Перевірка базується винятково на 12-бальних оцінках!
            if (grades != null && grades.All(g => g == 12))
            {
                AutomatReceived?.Invoke(this, EventArgs.Empty);
            }
        }


        // Подія з generic делегатом EventHandler<TEventArgs>,
        // яка виникає при досягненні середнього балу >=10.
        public event EventHandler<SchArgs>? ScholarshipAwarded;
        // Можна передати власні дані події: середній бал і сума стипендії.

        // Метод перевірки права на стипендію і генерація події з даними
        public void CheckScholarship()
        {
            if (AverageGrade >= 10.0)
            {
                // Передаємо власний об'єкт з даними про середній бал
                ScholarshipAwarded?.Invoke(this, new SchArgs(AverageGrade));
            }
        }
    }
}