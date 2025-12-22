using System;

namespace student_life
{
    class Program
    {
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
                new Student(
                    "Шухевич", "Роман", "Осипович",
                    new DateTime(1907, 6, 30), "Львів", "",
                    [12, 10, 10, 12, 12, 11],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),

                new Student(
                    "Лемик", "Микола", "Семенович",
                    new DateTime(1915, 4, 4), "Солова", "",
                    [11, 11, 11, 11, 11, 11],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),

                new Student(
                    "Ленкавський", "Степан", "Володимирович",
                    new DateTime(1904, 7, 6), "Угорники", "",
                    [12, 11, 12, 12, 12, 12],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),

                new Student(
                    "Мирон", "Дмитро", "Орлик",
                    new DateTime(1911, 11, 5), "Рай", "",
                    [10, 11, 12, 12, 10, 10],
                    ["Python", "HTML/CSS", "SQL", "C++", "JavaScript", "React"],
                    [true, true, true, true, true, true]
                    ),

                new Student(
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
        }
    }
}