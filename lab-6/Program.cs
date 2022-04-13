using System;
using System.Collections.Generic;

namespace lab_6
{
    class Student
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Student Equals");
            return obj is Student student &&
                   Name == student.Name &&
                   Ects == student.Ects;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Student GetHashCode");
            return HashCode.Combine(Name, Ects);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<string> names = new List<string>();
            names.Add("ewa");
            names.Add("karol");
            names.Add("adam");
            names.Add("adam");
            foreach(string name in names)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine(names.Contains("ewa"));
            Console.WriteLine(names.Remove("adam"));

            Console.WriteLine();
            //wykonaj podobne operacje na kolekcji studentów


            ICollection<Student> students = new List<Student>();
            students.Add(new Student {Name = "ewa", Ects = 13 });
            students.Add(new Student {Name = "ania", Ects = 9 });
            students.Add(new Student {Name = "ania", Ects = 9 });
            students.Add(new Student {Name = "tomek", Ects = 6 });

            foreach (var student in students)
            {
                Console.WriteLine(student.Name + " " + student.Ects);
            }
            Console.WriteLine(students.Contains(new Student { Name = "tomek", Ects = 6}));
            Console.WriteLine(students.Remove(new Student { Name = "ania", Ects = 9 }));

            List<Student> list = (List<Student>) students;
            //list.Insert(1, new Student() { Name = "ania", Ects = 44 });
            list.RemoveAt(2);
            for (int i = 0; i< list.Count; i++)
            {
                Console.WriteLine(list[i].Name);
            }

            Console.WriteLine();

            ISet<string> set = new HashSet<string>();
            set.Add("ewa");
            set.Add("karol");
            set.Add("ania");
            Console.WriteLine(set.Contains("ewa"));
            Console.WriteLine(set.Remove("ewa"));
            foreach(string name in set)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("---------------------");

            ISet<Student> group = new HashSet<Student>(list);
            group.Add(new Student() { Name = "ewa", Ects = 14 });
            foreach(var s in group)
            {
                Console.WriteLine(s.Name+" "+ s.Ects);
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine(group.Contains(new Student() { Name = "tomek", Ects = 6 }));

            ISet<int> s1 = new HashSet<int>(new int[] { 1,2,5,6,7});
            ISet<int> s2 = new HashSet<int>(new int[] { 4, 7, 9, 8, 3 });
            s1.IntersectWith(s2);
            Console.WriteLine(string.Join(", ", s1));

            Console.WriteLine("-------------------------------------");

            Dictionary<string, string> phoneBook = new Dictionary<string, string>();
            phoneBook.Add("adam", "234234234");
            phoneBook["ewa"] = "456456456";
            phoneBook["karol"] = "123123123";
            Console.WriteLine(phoneBook["ewa"]);
            foreach(var item in phoneBook)
            {
                Console.WriteLine(item.Key+" "+ item.Value);
            }

            Dictionary<string, object> semiObj = new Dictionary<string, object>();
            semiObj["name"] = "adam";
            semiObj["points"] = 45;
            semiObj["student"] = list[0];
            string[] arr = { "ewa", "adam", "ewa", "karol", "ania", "ewa", "adam" };
            //podaj ile razy wystepuje kazde imie w tabeli arr
            Dictionary<int, int> count = new Dictionary<int, int>();
            foreach(var item in arr)
            {
                count.ContainsKey()
            }
            
        }
    }
}
