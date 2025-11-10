// See https://aka.ms/new-console-template for more information
static void printPerson((string FirstName, string LastName, int Age) person)
{
    Console.WriteLine($"First Name: {person.FirstName}");
    Console.WriteLine($"Last Name: {person.LastName}");
    Console.WriteLine($"Age: {person.Age}");
}


var person = ("John", "Doe", 30);
Console.WriteLine($"First Name: {person.Item1}");
var (firstName, lastName, age) = ("Filip", "Kowalski", 25);
Console.WriteLine($"Last Name: {lastName}");
(string FirstName, string LastName, int Age) anotherPerson = ("Anna", "Nowak", 28);
Console.WriteLine($"Age: {anotherPerson.Age}");
printPerson(person);

var @class = 5;
Console.WriteLine(@class);