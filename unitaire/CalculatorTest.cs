using console;
namespace unitaire;

public class CalculatorTest
{
    [Theory]
    [InlineData(10,20,15)]
    [InlineData(0,0,0)]
    public void CalculateAverage_PositifNumber_ReturnAverage(double x, double y, double expectedResult)
    {
        //arrange
        var calculator = new Calculator();
        

        //act
        var actualAverage =calculator.CalculateAverage(x, y);

        //assert
        Assert.Equal(expectedResult, actualAverage); 
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