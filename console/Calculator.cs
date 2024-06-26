namespace console;

public class Calculator
{
    public double CalculateAverage (double x, double y)
    {
        if (x >=0 && y >= 0)
        {
            return (x + y) / 2;
        }
        throw new ArgumentException("toutes les valeurs doient être égale ou supérieur a zéros");
    }

}
