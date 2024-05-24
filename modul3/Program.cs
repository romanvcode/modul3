using System.Xml.Linq;
using System.Xml.Serialization;

namespace modul3
{
    //Напишіть програму на C#, яка виконує наступні дії:
    //Зчитує дані з файлу employees.xml.Файл містить список співробітників у форматі XML, де кожен співробітник має такі властивості: Name, Position, HireDate.
    //Сортує співробітників за датою прийому на роботу(від найстаріших до найновіших) за допомогою LINQ.
    //Зберігає відсортований список співробітників у новий XML файл sorted_employees.xml.
    //Записує інформацію про співробітників в текстовий файл employees.txt у наступному форматі:

    //Name: [Name] Position: [Position] HireDate: [HireDate]

    [XmlRoot("Employees")]
    public class Employees
    {
        [XmlElement("Employee")]

        public List<Employee>? Employee { get; set; }
    }

    [Serializable]
    public class Employee
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? HireDate { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} Position: {Position} HireDate: {HireDate}";
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Employees));
            Employees employees;

            using (StreamReader reader = new StreamReader("D:\\Projects\\C#\\Programming\\modul3\\modul3\\employees.xml"))
            {
                employees = (Employees)serializer.Deserialize(reader);
            }

            foreach (var employee in employees.Employee)
            {
                Console.WriteLine(employee);
            }

            var sortedEmployees = employees.Employee.OrderBy(e => e.HireDate).ToList();

            using(StreamWriter writer = new StreamWriter("D:\\Projects\\C#\\Programming\\modul3\\modul3\\employees.txt"))
            {
                foreach (var employee in sortedEmployees)
                {
                    writer.WriteLine(employee);
                }
            }

            using(StreamWriter writer = new StreamWriter("D:\\Projects\\C#\\Programming\\modul3\\modul3\\sorted_employees.xml"))
            {
                serializer.Serialize(writer, new Employees { Employee = sortedEmployees });
            }
        }
    }
}
