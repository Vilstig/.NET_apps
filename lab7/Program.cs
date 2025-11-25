// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Collections.Generic;
using LectureClasses;
using System.Reflection;

class Program
{
    public static IEnumerable<List<StudentWithTopics>> GroupStudents(List<StudentWithTopics> students, int n)
    {
        return students
            .OrderBy(s => s.Name)        // sortowanie po nazwisku
            .ThenBy(s => s.Index)        // sortowanie po indeksie gdy nazwiska są takie same
            .Select((student, index) => new { student, index })
            .GroupBy(x => x.index / n)   // dzielenie na grupy po n
            .Select(g => g.Select(x => x.student).ToList());
    }

    static void Zad1()
    {
        var students = Generator.GenerateStudentsWithTopicsEasy();
        var groups = GroupStudents(students, 3);

        int i = 1;
        foreach (var group in groups)
        {
            Console.WriteLine($"Group {i++}:");
            foreach (var s in group)
                Console.WriteLine("  " + s);
            Console.WriteLine();
        }

    }

    static void Zad2()
    {
        var students = Generator.GenerateStudentsWithTopicsEasy();
        var topics = students
                    .SelectMany(t => t.Topics)
                    .GroupBy(topic => topic)         
                    .Select(g => new                 
                    {
                        Topic = g.Key,               
                        Count = g.Count()            
                    })
                    .OrderByDescending(g => g.Count);

        var topicsByGender = students
                    .GroupBy(s => s.Gender)                     
                    .Select(g => new
                    {
                        Gender = g.Key,
                        Topics = g
                            .SelectMany(s => s.Topics)          
                            .GroupBy(t => t)                  
                            .Select(tg => new
                            {
                                Topic = tg.Key,
                                Count = tg.Count()
                            })
                            .OrderByDescending(x => x.Count)   
                    });


        int i = 1;
        foreach (var topic in topics)
        {
            Console.WriteLine($"Topic {i++}: {topic}");
            Console.WriteLine();
        }

        
        foreach (var group in topicsByGender)
        {
            Console.WriteLine($"Gender: {group.Gender}");
            int j = 1;
                foreach (var topic in group.Topics)
                {
                    Console.WriteLine($"Topic {j++}: {topic}");
                }
            Console.WriteLine();
        }
    }

    public static List<Topic> ExtractTopics(List<StudentWithTopics> students) => students
                .SelectMany(s => s.Topics)
                .Distinct()
                .Select((topic, index) => new Topic(index + 1, topic))
                .ToList();

    public static List<Student> studentWithTopicsToStudents(List<StudentWithTopics> students)
    {
        var topics = ExtractTopics(students);

        var topicIdByName = topics.ToDictionary(t => t.Name, t => t.Id);

        var result = new List<Student>();

        foreach (var s in students)
        {
            var topicIds = s.Topics
                .Select(name => topicIdByName[name])
                .ToList();

            var student = new Student(
                s.Id,
                s.Index,
                s.Name,
                s.Gender,
                s.Active,
                s.DepartmentId,
                topicIds
            );

            result.Add(student);
        }

        return result;
    }

    static void Zad3()
    {
        var students = Generator.GenerateStudentsWithTopicsEasy();

        var newStudents = studentWithTopicsToStudents(students);


        foreach (var student in newStudents)
        {
            Console.WriteLine($"{student}");
        }
    }

    static void Zad4()
    {
        string className = "LectureClasses.Topic";

        Type type = Type.GetType(className);

        object obj1 = Activator.CreateInstance(type, 1, "C#");

        object obj2 = Activator.CreateInstance(type, 2, "Java");

        MethodInfo method = type.GetMethod("ToString");

        object result = method.Invoke(obj1, null);

        Console.WriteLine(result);
    }
    static void Main(string[] args)
    {
        //Zad1();
        //Zad2();
        //Zad3();
        Zad4();
    }
}
