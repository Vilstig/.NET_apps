// See https://aka.ms/new-console-template for more information
using System.ComponentModel;

static void DrawCard(string firstLine, string secondLine, char fillChar, int borderWidth, int width)
{
    int height = borderWidth * 2 + 2;

    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            if (i < borderWidth || i >= height - borderWidth || j < borderWidth || j >= width - borderWidth)
            {
                Console.Write(fillChar);
            }
            else if (i)
        }
    }
}