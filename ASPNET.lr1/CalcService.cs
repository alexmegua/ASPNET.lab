namespace MyCalcApp.Services
{
    public class CalcService
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        public int Multiply(int a, int b) => a * b;
        public int Divide(int a, int b) => b != 0 ? a / b : throw new DivideByZeroException("Ділення на нуль неможливе");
    }
}

