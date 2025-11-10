// See https://aka.ms/new-console-template for more information


static void DrawCard(string firstLine, string secondLine = "Rys", char fillChar = '#', int borderWidth = 2, int width = 20)
{
    int height = borderWidth * 2 + 2;

    if (firstLine.Length > width + borderWidth * 2 || secondLine.Length > width + borderWidth * 2)
    {
        Console.WriteLine("Error: Text lines are too long for the specified width and border.");
        return;
    }

    if (borderWidth < 1 || width < 2)
    {
        Console.WriteLine("Error: Border width must be at least 1 and width must be at least 2.");
        return;
    }

    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            if (i < borderWidth || i >= height - borderWidth || j < borderWidth || j >= width - borderWidth)
            {
                Console.Write(fillChar);
            }
            else if (j == width / 2 - firstLine.Length / 2 && i == borderWidth)
            {
                Console.Write(firstLine);
                j += firstLine.Length - 1;
            }
            else if (j == width / 2 - secondLine.Length / 2 && i == borderWidth + 1)
            {
                Console.Write(secondLine);
                j += secondLine.Length - 1;
            }
            else
            {
                Console.Write(' ');
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

static (int, int, int, int) CountMyTypes<T>(T[] array)
{
    int evenIntCount = 0;
    int longerThan4StringCount = 0;
    int positiveDoubleCount = 0;
    int otherCount = 0;

    foreach (var item in array)
    {
        switch (item)
        {
            case int i when i % 2 == 0:
                evenIntCount++;
                break;
            case string s when s.Length > 4:
                longerThan4StringCount++;
                break;
            case double d when d >= 0:
                positiveDoubleCount++;
                break;
            default:
                otherCount++;
                break;
        }
    }
    return (evenIntCount, longerThan4StringCount, positiveDoubleCount, otherCount);
}

DrawCard("Ryszard", "Rys", 'X', 2, 20);

DrawCard("Kacper");

DrawCard(secondLine: "Nowak", firstLine: "Witek", fillChar: '*', width: 30, borderWidth: 3);

object[] myArray = [2, "Hell", -3.5, 4.2, 7, "World", 10, 0.0, 5.5f, 'A'];
var (evenInts, strings, positiveDoubles, others) = CountMyTypes(myArray);
Console.WriteLine($"Even integers: {evenInts}");
Console.WriteLine($"Strings longer than 4 characters: {strings}");
Console.WriteLine($"Positive doubles: {positiveDoubles}");
Console.WriteLine($"Other types: {others}");