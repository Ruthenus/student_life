using System;
using System.Collections;
using System.Collections.Generic;
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic?view=net-9.0

namespace student_life
{
    public class Group : IEnumerable<Student>
    // Реалізація інтерфейсу IEnumerable<Student> дозволяє використовувати
    // конструкцію foreach для перебору студентів групи:
    // foreach (Student s in group) { ... }
    // https://learn.microsoft.com/ru-ru/dotnet/api/system.collections.generic.ienumerable-1?view=net-10.0
    {
        private List<Student> students = [];  // список студентів
        private string groupName = "СПР411";     // назва групи
        private string specialization = "Розробка програмного забезпечення";
        private int courseNumber = 1;            // номер курсу


        // Приватні методи встановлення значень приватних полів
        private void SetStudents(List<Student>? value)
        {
            students = value != null
                ? [.. value]  // ? new List<Student>(value)
                : students;   // : new List<Student>();
        }
        private void SetGroupName(string value)
        {
            groupName = value ??
                string.Empty;
        }
        private void SetSpecialization(string value)
        {
            specialization = value
                ?? string.Empty;
        }
        private void SetCourseNumber(int value) { courseNumber = value; }


        // Конструктор без параметрів - налаштовує порожню групу:
        public Group()
        {
            SetStudents(null);
            SetGroupName(string.Empty);
            SetSpecialization(string.Empty);
            SetCourseNumber(-1);
        }


        // Конструктор на основі наявної колекції студентів
        public Group(List<Student> students, string groupName,
                     string specialization, int courseNumber)
        {
            SetStudents(students);
            SetGroupName(groupName);
            SetSpecialization(specialization);
            SetCourseNumber(courseNumber);
        }


        // Геттери
        public List<Student> GetStudents() { return [.. students]; }
        public string GetGroupName() { return groupName; }
        public string GetSpecialization() { return specialization; }
        public int GetCourseNumber() { return courseNumber; }


        // Конструктор копіювання - створює нову групу як копію іншої
        public Group(Group other)
        {
            SetStudents(other.GetStudents());
            SetGroupName(other.GetGroupName());
            SetSpecialization(other.GetSpecialization());
            SetCourseNumber(other.GetCourseNumber());
        }


        // Властивості
        public int Count  // кількість студентів у групі
        {
            get { return students.Count; }
        }

        public string Specialization  // спеціалізація
        {
            get { return specialization; }
            set { SetSpecialization(value); }
        }

        public int Course  // курс, на якому навчаються студенти групи
        {
            get { return courseNumber; }
            set { SetCourseNumber(value); }
        }


        // Індексатор за номером – як на читання, так і на запис
        // https://learn.microsoft.com/ru-ru/dotnet/csharp/programming-guide/indexers/
        public Student this[int index]
        {
            get
            {
                if (index < 0 || index >= students.Count)
                {
                    throw new IndexOutOfRangeException("Індекс " +
                        "поза межами групи!");
                }
                // https://learn.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception?view=net-10.0
                // Повертаємо за індексом у межах групи
                return students[index];
            }

            set
            {
                if (index < 0)
                {
                    throw new IndexOutOfRangeException("Індекс не може " +
                        "бути від'ємним!");
                }
                // Дозволяємо додавання студента в кінець групи:
                if (index == students.Count)
                {
                    students.Add(value);
                    return;
                }
                if (index > students.Count)
                {
                    throw new IndexOutOfRangeException(
                        $"Неможливо встановити студента на позицію {index}. " +
                        $"Поточна кількість студентів: {students.Count}. " +
                        $"Використовуйте index == {students.Count} " +
                        $"для додавання.");
                }
                // Натомість звичайна заміна
                students[index] = value;
            }
        }

        // Індексатор за прізвищем (тільки читання)
        public Student? this[string surname]
        {
            get
            {
                // Перевірка, чи рядок не є null, порожнім або чи не
                // складається лише з самих пробілів
                if (string.IsNullOrWhiteSpace(surname))
                // https://learn.microsoft.com/dotnet/api/system.string.isnullorwhitespace
                {
                    return null;
                }

                // Перебір усіх студентів у колекції
                foreach (Student s in students)
                {
                    // Порівняння прізвищ, не зважаючи на культурні особливості
                    // https://learn.microsoft.com/dotnet/api/system.string.equals
                    if (string.Equals(s.GetSurname(), surname,
                        StringComparison.OrdinalIgnoreCase))
                    // https://learn.microsoft.com/dotnet/api/system.stringcomparison
                    {
                        return s;
                    }
                }

                // Якщо жодного збігу не знайдено — повертаємо null
                return null;
            }
        }


        // Перевантаження операторів
        public static bool operator ==(Group? a, Group? b)
        {
            if (a is null || b is null) return false;
            return a.Count == b.Count;
        }

        public static bool operator !=(Group? a, Group? b)
        {
            return !(a == b);
        }


        // Перевизначення Equals для порівняння з іншим екземпляром Group
        public override bool Equals(object? obj)
        {
            // Якщо obj — Group, порівняти через перевантажений оператор ==
            return obj is Group g && this == g;
        }

        // Хеш-код групи визначається кількістю студентів у ній (Count)
        public override int GetHashCode()
        {
            return Count.GetHashCode();
        }


        // Метод для виведення інформації про групу та списку студентів
        public void DisplayGroup()
        {
            Console.WriteLine($"Група: {groupName}");
            Console.WriteLine($"Спеціалізація: {specialization}");
            Console.WriteLine($"Курс: {courseNumber}");
            Console.WriteLine("Список студентів:");

            // Копіюємо список студентів для сортування
            List<Student> sorted = [.. students];

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
                    + s.GetName() + " " + s.AverageGrade.ToString("F2"));
                index++;
            }
        }


        // Метод додавання студента до групи
        public void AddStudent(Student student)
        {
            if (student != null && !students.Contains(student))
            {
                students.Add(student);
            }
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
            List<Student> passed = [];

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


        // Реалізація типізованого IEnumerable<Student>.GetEnumerator()
        // Цей метод повертає об'єкт (власний енумератор), який знає,
        // як послідовно перебирати елементи колекції.
        // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1.getenumerator
        public IEnumerator<Student> GetEnumerator()
        {
            return new GroupEnumerator(students);
        }


        // Реалізація нетипізованого IEnumerable.GetEnumerator()
        // Цей метод потрібен для сумісності зі старим нетипізованим
        // інтерфейсом IEnumerable.
        // https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable.getenumerator?view=net-10.0
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        // Вкладений клас-енумератор для перелічення студентів.
        // Використовуємо первинний конструктор (C# 12), щоб усунути
        // попередження IDE0290 та зробити код більш лаконічним і сучасним.
        // Параметр students захоплюється і автоматично присвоюється
        // полю _students.
        private class GroupEnumerator(List<Student> students)
            : IEnumerator<Student>  // визначає механізм переміщення колекцією
        // https://learn.microsoft.com/ru-ru/dotnet/api/system.collections.generic.ienumerator-1?view=net-9.0
        {
            // Зберігаємо посилання на список студентів
            private readonly List<Student> _students = students;
            // Поточна позиція в списку
            private int curIndex = -1;  // перед першим елементом
            // Кеш поточного студента (для властивості Current)
            private Student? curStudent = null;

            // Метод переходу до наступного елемента
            // Повертає true, якщо перехід успішний (є наступний елемент).
            public bool MoveNext()
            {
                // Збільшуємо індекс і перевіряємо, чи не вийшли за межі
                if (++curIndex >= _students.Count)
                    return false;  // більше елементів немає
                else
                {
                    // Зберігаємо поточного студента для доступу через Current
                    curStudent = _students[curIndex];
                }
                return true;
            }

            // Метод повернення на початок перебору
            // Потрібен для повторного використання енумератора
            // (наприклад, у кількох foreach):
            public void Reset()
            {
                curIndex = -1;
            }

            // Метод звільнення ресурсів
            // У нашому випадку ресурсів немає, тому метод порожній.
            // Реалізація IDisposable потрібна за контрактом інтерфейсу
            // IEnumerator.
            void IDisposable.Dispose() { }
            // https://learn.microsoft.com/ru-ru/dotnet/fundamentals/runtime-libraries/system-idisposable

            // Поточний елемент (типізована версія) - повертає поточного
            // студента. Кидає виняток, якщо позиція некоректна.
            public Student Current
            {
                get
                {
                    // Захист від некоректного використання (до MoveNext
                    // або після кінця)
                    if (curIndex < 0 || curIndex >= _students.Count)
                        throw new InvalidOperationException(
                             "Енумератор не перебуває в дійсній позиції. " +
                             "Викличіть MoveNext() спочатку!");
                    return curStudent!;  // ! - упевненість,
                    // що curStudent не null у валідній позиції
                }
            }

            // Поточний елемент (нетипізована версія) - явна реалізація
            // для інтерфейсу IEnumerator. Повертає той самий об'єкт,
            // але як object.
            object IEnumerator.Current
            {
                get { return Current; } // делегуємо до типізованої властивості
            }
        }
    }
}