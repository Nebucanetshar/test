using console;
namespace unitaire;

public class CalculatorTest
{
    [Fact]
    public void CalculateAverage_PositifNumber_ReturnAverage()
    {
        //arrange
        var calculator = new Calculator();
        double x = 10, y = 20;

        //act
        var actualAverage =calculator.CalculateAverage(x, y);

        //assert
        Assert.Equal(15, actualAverage); 
    }

    [Fact]
    public void CalculateAverage_NegatifNumber_ThrowException()
    {
        // arrange
        var calculator = new Calculator();
        double x= -10 , y= -20;

        //act & assert
        Assert.Throws<ArgumentException>(()=>calculator.CalculateAverage(x, y));


    }
}