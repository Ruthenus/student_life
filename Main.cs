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
        }
    }
}