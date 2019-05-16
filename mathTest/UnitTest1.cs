using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mathTest
{
    [TestClass]
    public class Fraction
    {
        [TestMethod]
        public void TestRegex()
        {
            var fraction1 = new math.Program.Fraction("1/2");
            var fraction2 = new math.Program.Fraction("3_1/2");
            var fraction3 = new math.Program.Fraction("3");

            var fractionA = new math.Program.Fraction(0, 1, 2);
            var fractionB = new math.Program.Fraction(3, 1, 2);
            var fractionC = new math.Program.Fraction(3, 0, 1);


            Assert.AreEqual(fraction1.Whole, fractionA.Whole );
            Assert.AreEqual(fraction1.Denominator, fractionA.Denominator);
            Assert.AreEqual(fraction1.Numerator, fractionA.Numerator);

            Assert.AreEqual(fraction2.Whole, fractionB.Whole);
            Assert.AreEqual(fraction2.Denominator, fractionB.Denominator);
            Assert.AreEqual(fraction2.Numerator, fractionB.Numerator);

            Assert.AreEqual(fraction3.Whole, fractionC.Whole);
            Assert.AreEqual(fraction3.Denominator, fractionC.Denominator);
            Assert.AreEqual(fraction3.Numerator, fractionC.Numerator);
        }
        [TestMethod]
        public void TestMath()
        {
            var fraction1 = new math.Program.Fraction("1/2");
            var fraction2 = new math.Program.Fraction("3_1/2");
            var fraction3 = new math.Program.Fraction("3");

            fraction1.Add(fraction3);
            Assert.AreEqual(fraction1.ToString(), fraction2.ToString());

            fraction1 = new math.Program.Fraction("1/2");
            fraction2 = new math.Program.Fraction("2_1/2");
            fraction3 = new math.Program.Fraction("3");

            fraction3.Subtract(fraction1);
            Assert.AreEqual(fraction3.ToString(), fraction2.ToString());

            fraction1 = new math.Program.Fraction("1/2");
            fraction2 = new math.Program.Fraction("1/2");
            fraction3 = new math.Program.Fraction("1/4");

            fraction1.Multiply(fraction2);
            Assert.AreEqual(fraction3.ToString(), fraction1.ToString());

            fraction1 = new math.Program.Fraction("1/2");
            fraction2 = new math.Program.Fraction("1/2");
            fraction3 = new math.Program.Fraction("1/4");

            fraction3.Divide(fraction1);
            Assert.AreEqual(fraction3.ToString(), fraction2.ToString());

        }
        [TestMethod]
        public void TestProgram()
        {
            string[] args = new string[] { "1_1/2", "+", "1_1/2" };
            Assert.AreEqual("3", math.Program.DoMath(args));

            args = new string[] { "1_1/2" };
            Assert.AreEqual("1_1/2", math.Program.DoMath(args));

            args = new string[] { "1_1/2", "+", "1_1/2", "+", "1_1/2", "+", "1_1/2" };
            Assert.AreEqual("6", math.Program.DoMath(args));

            args = new string[] { "1/2", "*", "1/32", "/", "6/32", "+", "1"};
            Assert.AreEqual("1_1/12", math.Program.DoMath(args));
        }
    }
}
