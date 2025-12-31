using System;

namespace student_life
{
    class Program
    {
        // Обробники подій — методи, що відповідають сигнатурі делегата,
        // та викликаються автоматично під час генерації відповідної події.

        // Обробник для події LectureMissed (власний делегат)
        static void OnLectureMissed(object? sender, EventArgs e)
        {
            var s = sender as Student;
            Console.WriteLine($"Шановний пане студенте {s?.LastName}! " +
                $"Негайно вмикайте радіостанцію!");
        }

        // Обробник для стандартної події AutomatReceived з EventHandler
        static void OnAutomatReceived(object? sender, EventArgs e)
        {
            var s = sender as Student;
            Console.WriteLine($"{s?.Name} {s?.LastName} отримав АВТОМАТ! " +
                $"Час відсвяткувати кавою!");
        }

        // Обробник для generic події ScholarshipAwarded
        static void OnScholarshipAwarded(object? sender,
            ScholarshipAwardedEventArgs e)
        {
            var s = sender as Student;
            Console.WriteLine($"Вітаємо, студенте {s?.LastName}! " +
                $"Із середнім балом {e.AverageGrade:F2} Ви гідні отримувати" +
                $" {e.ScholarshipAmount} гривень стипендії щомісяця!");
        }

        // Обробники подій для групи студентів
        static void OnGroupPartyPlanned(object? sender, EventArgs e)
        {
            // Пропозиція банкета при 100% відмінниках
            Console.WriteLine("Піца на всіх!!!");
        }

        static void OnSessionSurvived(object? sender, EventArgs e)
        {
            if (sender is Group group)
            {
                Console.WriteLine("Хай живе Степан Бандера та його Держава!");
                Console.WriteLine($"Сесія позаду! Час погуляти 2,5 години.");
            }
        }


        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ВЛАСТИВОСТІ, ІНДЕКСАТОРИ");

            var student = new Student
            {
                Name = "Роберт",
                LastName = "Олійник"
            };

            Console.WriteLine("\nСтудент, створений через властивості:");
            Console.WriteLine($"    Прізвище:      {student.LastName}");
            Console.WriteLine($"    Ім'я:          {student.Name}");
            Console.WriteLine($"    Вік:           {student.Age} років");
            Console.WriteLine($"    Середній бал:  {student.AverageGrade:F2}");

            // Виправляємо вік, додаємо академічну інформацію
            student = new Student(
                "Олійник", "Роберт", "",
                new DateTime(1911, 3, 9), "Мюнхен", "",
                [12, 12, 12, 12, 12, 12],
                ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                [true, true, true, true, true, true]
                );

            var group = new Group
            {
                Specialization = "Розробка програмного забезпечення",
                Course = 1
            };

            Console.WriteLine("\nГрупа, налаштована через властивості:");
            Console.WriteLine($"    Спеціалізація:        {group.
                Specialization}");
            Console.WriteLine($"    Курс:                 {group.Course}");
            Console.WriteLine($"    Кількість студентів:  {group.Count}");

            // group[-1] = student; // Індекс не може бути від'ємним
            // group[2] = student;  // Неможливо встановити студента на позицію
            group[group.Count] = student;
            group[group.Count] = new Student();  // конструктор без параметрів
            Console.WriteLine($"\nТепер кількість студентів у групі: " +
                $"{group.Count}");

            var found = group["Качуровський"];  // знайдено за прізвищем
            if (found != null)
            {
                Console.WriteLine($"{found.LastName} здобуває професійну " +
                    $"комп'ютерну освіту.");
            }

            Console.WriteLine("\n\nІНТЕРФЕЙСИ");

            // Записуємо 5 студентів з різними оцінками
            var studentsToAdd = new List<Student>
            {
                new(
                    "Шухевич", "Роман", "Осипович",
                    new DateTime(1907, 6, 30), "Львів", "",
                    [12, 10, 10, 12, 12, 11],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),

                new(
                    "Лемик", "Микола", "Семенович",
                    new DateTime(1915, 4, 4), "Солова", "",
                    [11, 11, 11, 11, 11, 11],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),

                new(
                    "Ленкавський", "Степан", "Володимирович",
                    new DateTime(1904, 7, 6), "Угорники", "",
                    [12, 11, 12, 12, 12, 12],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),

                new(
                    "Мирон", "Дмитро", "Орлик",
                    new DateTime(1911, 11, 5), "Рай", "",
                    [10, 11, 12, 12, 10, 10],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),

                new(
                    "Білас", "Василь", "Дмитрович",
                    new DateTime(1911, 9, 17), "Трускавець", "",
                    [11, 12, 11, 11, 12, 12],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),
            };

            // Додаємо їх до тієї ж групи
            foreach (var s in studentsToAdd)
            {
                group.AddStudent(s);
            }

            Console.WriteLine($"\nДодано ще {studentsToAdd.Count} студентів." +
                $" Загальна кількість: {group.Count}");

            // Демонстрація IEnumerable - простий foreach
            Console.WriteLine("\nПерелік студентів групи через foreach - " +
                "працює завдяки IEnumerable<Student>:");
            foreach (Student s in group)
            {
                Console.WriteLine($"    {s.GetName()} {s.GetSurname()}: " +
                    $"середній бал = {s.AverageGrade:F2}");
            }

            // Сортування за середнім балом (зростання), у разі рівних
            // балів - за ПІБ
            var sortedByGrade = group.GetStudents();
            sortedByGrade.Sort(new Student.AverageGradeComparer());

            Console.WriteLine("\nСортування за зростанням середнього балу " +
                "завдяки AverageGradeComparer:");
            for (int i = 0; i < sortedByGrade.Count; i++)
            {
                var s = sortedByGrade[i];
                Console.WriteLine($"    {i + 1}. {s.GetName()} " +
                    $"{s.GetSurname()}: середній бал = " +
                    $"{s.AverageGrade:F2};");
            }

            // Сортування за ПІБ (алфавітно), у разі однакового ПІБ -
            // за середнім балом (спадання)
            var sortedByName = group.GetStudents();
            sortedByName.Sort(new Student.FullNameComparer());

            Console.WriteLine("\nСортування прізвищ в алфавітному порядку " +
                "завдяки FullNameComparer:");
            for (int i = 0; i < sortedByName.Count; i++)
            {
                var s = sortedByName[i];
                Console.WriteLine($"    {i + 1}. {s.GetName()} " +
                    $"{s.GetSurname()}: середній бал = " +
                    $"{s.AverageGrade:F2};");
            }

            // Ще раз ітерація — оригінальний порядок у групі не змінився
            Console.WriteLine("\nПовторна ітерація через foreach:");
            int index = 1;
            foreach (Student s in group)
            {
                Console.WriteLine($"    {index++}. {s.GetName()} " +
                    $"{s.GetSurname()}: середній бал = " +
                    $"{s.AverageGrade:F2};");
            }

            Console.WriteLine("\n\nПОДІЇ\n");

            // Демонстрація подій на рівні студента
            foreach (Student s in group.GetStudents())
            {
                // Підписуємось на індивідуальні події студента
                s.LectureMissed += OnLectureMissed;
                s.AutomatReceived += OnAutomatReceived;
                s.ScholarshipAwarded += OnScholarshipAwarded;

                // Викликаємо перевірки — можуть генерувати події
                s.CheckTime();         // залежить від поточного часу (>08:55)
                s.CheckForAutomat();   // тільки якщо всі оцінки 12 балів
                s.CheckScholarship();  // якщо середній бал >= 10.0
                Console.WriteLine();
            }

            // Демонстрація подій групи студентів
            group.GroupPartyPlanned += OnGroupPartyPlanned;  // у всіх >= 10
            group.SessionSurvived += OnSessionSurvived;  // всі склали іспити

            // Перевірка сесії для всієї групи
            group.CheckSessionPassed();
            // SessionSurvived спрацює (всі склали іспити),
            // GroupPartyPlanned — якщо всі студенти мають всі оцінки >= 10.
        }
    }
}