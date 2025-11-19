using System;
using System.Collections.Generic;
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic?view=net-9.0

namespace student_life
{
    public class Group
    {
        private List<Student> students = new();  // список студентів
        private string groupName = "СПР411";     // назва групи
        private string specialization = "Розробка програмного забезпечення";
        private int courseNumber = 1;            // номер курсу

        // Конструктор без параметрів - налаштовує порожню групу:
        public Group()
        {
            students = new();
            groupName = string.Empty;
            specialization = string.Empty;
            courseNumber = -1;
        }

        // Конструктор на основі наявної колекції студентів
        public Group(List<Student> students, string groupName,
                     string specialization, int courseNumber)
        {
            this.students = new List<Student>(students);
            this.groupName = groupName;
            this.specialization = specialization;
            this.courseNumber = courseNumber;
        }

        // Конструктор копіювання - створює нову групу як копію іншої
        public Group(Group other)
        {
            students = new List<Student>(other.students);
            groupName = other.groupName;
            specialization = other.specialization;
            courseNumber = other.courseNumber;
        }

        // Метод для виведення інформації про групу та список студентів
        public void DisplayGroup()
        {
            Console.WriteLine($"Група: {groupName}");
            Console.WriteLine($"Спеціалізація: {specialization}");
            Console.WriteLine($"Курс: {courseNumber}");
            Console.WriteLine("Список студентів:");

            // Копіюємо список студентів для сортування
            List<Student> sorted = new(students);

            // Сортування за прізвищем, а потім за ім'ям (вручну)
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = i + 1; j < sorted.Count; j++)
                {
                    string surnameI = sorted[i].GetSurname() ?? string.Empty;
                    string surnameJ = sorted[j].GetSurname() ?? string.Empty;
                    string nameI = sorted[i].GetName() ?? string.Empty;
                    string nameJ = sorted[j].GetName() ?? string.Empty;

                    // Якщо прізвище I > J або однакові прізвища, але ім'я
                    // I > J — міняємо місцями
                    if (string.Compare(surnameI, surnameJ) > 0 ||
                        (surnameI == surnameJ &&
                        string.Compare(nameI, nameJ) > 0))
                    // https://learn.microsoft.com/ru-ru/dotnet/api/system.string.compare?view=net-10.0
                    {
                        (sorted[i], sorted[j]) = (sorted[j], sorted[i]);
                    }
                }
            }

            // Вивід відсортованого списку
            int index = 1;
            foreach (Student s in sorted)
            {
                Console.WriteLine(index + ". " + s.GetSurname() + " "
                    + s.GetName());
                index++;
            }
        }

        // Метод додавання студента до групи
        public void AddStudent(Student student)
        {
            if (!students.Contains(student))
                students.Add(student);
            // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=net-10.0
        }

        // Метод переведення студента до іншої групи
        public void TransferStudentTo(Student student, Group targetGroup)
        {
            if (student != null && targetGroup != null && 
                students.Contains(student))
            // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.contains?view=net-10.0
            {
                students.Remove(student);
                // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.remove?view=net-10.0
                targetGroup.AddStudent(student);
            }
        }


        // Метод відрахування студентів, які не склали хоча б один іспит
        public void ExpelFailedStudents()
        {
            // Список тих, хто склав всі іспити
            List<Student> passed = new();

            foreach (Student s in students)
            {
                // Отримуємо результати іспитів
                bool[]? exams = s.GetExamPassed();
                bool allPassed = true;

                if (exams != null)
                {
                    foreach (bool exam in exams)
                    {
                        if (!exam)
                        {
                            allPassed = false;
                            break;
                        }
                    }
                }
                else
                {
                    // Якщо немає даних — вважаємо, що не склав
                    allPassed = false;
                }

                if (allPassed)
                {
                    passed.Add(s);  // додаємо до нового списку

                }
            }

            students = passed;  // оновлюємо список студентів
        }

        // Метод відрахування студента з найгіршим середнім балом
        public void ExpelWorstStudent()
        {
            Student? worst = null; // змінна для збереження найгіршого студента
            // Початкове значення — максимально можливе, щоб знайти мінімум
            double worstAvg = double.MaxValue;
            // https://learn.microsoft.com/ru-ru/dotnet/api/system.double.maxvalue?view=net-10.0

            // Перебираємо всіх студентів у групі
            foreach (Student s in students)
            {
                // Отримуємо масив оцінок студента
                int[]? grades = s.GetCourseGrades();

                // Перевіряємо, що оцінки існують і не порожні
                if (grades != null && grades.Length > 0)
                {
                    int sum = 0;

                    // Обчислюємо суму всіх оцінок
                    foreach (int grade in grades)
                    {
                        sum += grade;
                    }

                    // Обчислюємо середній бал
                    double avg = (double)sum / grades.Length;

                    if (avg < worstAvg)
                    {
                        worstAvg = avg;
                        worst = s;
                    }
                }
            }

            // Якщо знайдено найгіршого студента — видаляємо його з групи
            if (worst != null)
            {
                students.Remove(worst);
            }
        }

        // Отримання списку студентів
        public List<Student> GetStudents()
            { return students; }
    }
}