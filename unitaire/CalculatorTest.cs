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

    [Theory]
    [InlineData(1, -1)]
    [InlineData(-10, 20)]
    public void CalculateAverage_NegatifNumber_ThrowException(double x, double y)
    {
        // arrange
        var calculator = new Calculator();
        

        //act & assert
        Assert.Throws<ArgumentException>(()=>calculator.CalculateAverage(x, y));
    }

    [Theory]
    [InlineData(1,2,3)]
    public void CalculateAverage_PositifNumber_ReturnSum(double x, double y, double expectedSum)
    {
        //arrange 
        var calculator= new Calculator();

        //act
        var actualSum =calculator.CalculateSum(x, y);

        //Assert
        Assert.Equal(expectedSum, actualSum);
    }
}