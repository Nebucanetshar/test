namespace console;

public class Calculator
{
    public double CalculateAverage (double x, double y)
    {
        return (x >=0 && y >= 0 ? (x + y) / 2 :
        throw new ArgumentException("toutes les valeurs doient être égale ou supérieur a zéros"));
    }

    public double CalculateSum(double x, double y)
    {
        return (x + y);
    }

    public double CalculateMarksAverage(Student student)
    {
        if (student.Note != null && student.Note.Any())
        {
            return student.Note.Average();
        }
        throw new ArgumentException("la note ne peut être null ou vide");
    }

}
