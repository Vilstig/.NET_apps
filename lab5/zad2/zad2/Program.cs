// See https://aka.ms/new-console-template for more information

string parameters;
Console.WriteLine("Enter coefficients:");
parameters = Console.ReadLine() ?? string.Empty;
try
{
    if (string.IsNullOrWhiteSpace(parameters) || parameters.Split(' ').Length != 3)
    {
        throw new ArgumentException("Invalid input. Please enter three coefficients separated by spaces.");
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
    return;
}
double[] coeffs = parameters.Split(' ').Select(double.Parse).ToArray();
solveQuadraticEquation(coeffs[0], coeffs[1], coeffs[2]);

static void solveQuadraticEquation(double a, double b, double c)
{
    if (a == 0)
    {
        if (b == 0)
        {
            Console.WriteLine(c == 0 ? "Infinite solutions." : "No solution.");
        }
        else
        {
            double x = -c / b;
            Console.WriteLine($"One real root: x = {0}", x);
        }
        return;
    }
    double delta = b * b - 4 * a * c;

    if (delta < 0)
    {
        Console.WriteLine("No real roots.");
    }
    else if (delta == 0)
    {
        double x = -b / (2 * a);
        Console.WriteLine($"One real root: x = {0}", x);
    }
    else
    {
        double x1 = (-b - Math.Sqrt(delta)) / (2 * a);
        double x2 = (-b + Math.Sqrt(delta)) / (2 * a);
        Console.WriteLine($"Two real roots: x1 = {x1}, x2 = {x2}");
    }
}


