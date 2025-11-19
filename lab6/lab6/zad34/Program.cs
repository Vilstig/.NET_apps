class Zad3
{
    static void PrintPerson(dynamic person)
    {
        Console.WriteLine($"First Name: {person.FirstName}");
        Console.WriteLine($"Last Name: {person.LastName}");
        Console.WriteLine($"Age: {person.Age}");
    }
    static void Zad4()
    {
        Console.WriteLine("\n============\nZadanie 4\n============\n");
        var person = new { FirstName = "John", LastName = "Doe", Age = 30 };
        PrintPerson(person);
    }

    static void FillArray(int[] array)
    {
        Random rand = new();   
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = rand.Next(0, 20);
        }
    }
    static void Main(string[] args)
    {
        const int length = 20;
        int[] numbers = new int[length];
        FillArray(numbers);

        int[] sortedNums = (int[])numbers.Clone();
        Array.Sort(sortedNums);

        Console.WriteLine("Original array:");
        Console.WriteLine(string.Join(", ", numbers));
        Console.WriteLine("Sorted array:");
        Console.WriteLine(string.Join(", ", sortedNums));
        Console.WriteLine($"Index of 3: {Array.BinarySearch(sortedNums, 3)}");
        Console.WriteLine($"Index of 15: {Array.BinarySearch(sortedNums, 15)}");

        Console.WriteLine($"Last element smaller than 10: {Array.FindLast(numbers, n => n < 10)}");

        int[] croppedNums = new int[length];
        croppedNums = (int[])numbers.Clone();
        Array.Resize(ref croppedNums, 10);
        Console.WriteLine("Cropped array:");
        Console.WriteLine(string.Join(", ", croppedNums));

        Console.WriteLine(numbers.ToString());

        Zad4();
    }
}

