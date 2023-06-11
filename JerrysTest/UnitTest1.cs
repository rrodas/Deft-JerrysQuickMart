namespace JerrysTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void AddingTwoNumbersThenExpectingResult()
    {
        //Arramge
        int a=3;
        int b=4;
        int expected = 7;
        int result =0;
        //Act
        result = Calc.Sum(a , b );
        Console.WriteLine(result.ToString());        
        Assert.AreEqual(expected, result);
    }
}

