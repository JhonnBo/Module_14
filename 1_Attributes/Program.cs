
namespace _1_Attributes
{
    internal class Program
    {
        interface IHuman
        {
            string Name { get; set; }
            string Surname { get; set; }
        }


        // Атрибуты в.NET представляют специальные инструменты,
        // которые позволяют встраивать в сборку дополнительные
        // сведения (метаданные) об элементах программы (классах, методах и т.д).
        // Атрибуты могут применяться как ко всему типу(классу, интерфейсу и т.д.), 
        // так и к отдельным его частям(методу, свойству и т.д.). 
        // Основу атрибутов составляет класс System.Attribute, 
        // от которого образованы все остальные классы атрибутов.

        // атрибут трансформации текста  в верхний/нижний регистр 
        public sealed class TextTransformAttribute : System.Attribute
        {
            public bool IsUpperCase { get; set; }  // true - в верхний регистр,  false - в нижний  
        }

        [TextTransform(IsUpperCase = false)]
        class Human : IHuman
        {
            public Human(string Name, string Surname)
            {
                this.Name = Name;
                this.Surname = Surname;
            }
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        /* 
         * С помощью атрибута TextTransform формируем метаданные  
         * с информацией о том, что Фамилия и Имя должны переводиться в верхний регистр. 
         */
        [TextTransform(IsUpperCase = true)]
        class Student : IHuman
        {
            public Student(string Name, string Surname, double GPA)
            {
                this.Name = Name;
                this.Surname = Surname;
                this.GPA = GPA;
            }
            public string Name { get; set; }
            public string Surname { get; set; }
            public double GPA { get; set; }
        }

        /* Класс HumanPrinter - выводит в консоль Фамилию и Имя  объектов, реализующих IHuman.  
         * При выводе происходит проверка на необходимость трансформации  выводимого текста в 
         * верхний/нижний регистр
         */
        class HumanPrinter
        {
            public void Show(IHuman h)
            {
                string name = h.Name;
                string surname = h.Surname;
                // При использовании атрибутов в программном коде, метаданные остаются не используемыми 
                // до тех пор пока другой фрагмент программного кода не будет явно использовать 
                // отображение данной информации
                // Проверка на атрибут TextTransform 
                Type type = h.GetType();
                object[] objects = type.GetCustomAttributes(typeof(TextTransformAttribute), false);
                if (objects.Length != 0)
                {
                    //TextTransform обнаружен, переводим в нужный регистр 
                    TextTransformAttribute TTA = objects[0] as TextTransformAttribute;
                    name = (TTA.IsUpperCase) ? name.ToUpper() : name.ToLower();
                    surname = (TTA.IsUpperCase) ? surname.ToUpper() : surname.ToLower();
                }
                Console.WriteLine("Имя      : {0}", name);
                Console.WriteLine("Фамилия  : {0}", surname);
            }
        }

        static void Main(string[] args)
        {
            Human human = new Human("Иван", "Иванов");
            Student student = new Student("Петр", "Петров", 11.5);
            HumanPrinter hp = new HumanPrinter();
            hp.Show(human);  // Объект выводится без трансформации текста    
            hp.Show(student);  // Объект выводится с трансформацией текста 
        }
    }
}