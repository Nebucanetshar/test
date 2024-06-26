using console;

var calculator = new Calculator();
var average = calculator.CalculateAverage(10, 20);
Console.WriteLine(average);

average = calculator.CalculateAverage(-10, -20);
Console.WriteLine(average);
