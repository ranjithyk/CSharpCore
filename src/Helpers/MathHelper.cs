using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpCore.Helpers
{
    /// <summary>
    /// Process simple mathematical calculation
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class MathHelper
    {
        /// <summary>
        /// Calulate for single data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operationType"></param>
        /// <param name="paramA"></param>
        /// <param name="paramB"></param>
        /// <returns></returns>
        public static T Calculate<T>(MathOperationType operationType, T paramA, T paramB)
        {
            switch (operationType)
            {
                case MathOperationType.Addition:
                    return Number<T>.Calculator.Add(paramA, paramB);

                case MathOperationType.Sustraction:
                    return Number<T>.Calculator.Sub(paramA, paramB);

                case MathOperationType.Difference:
                    return Number<T>.Calculator.Difference(paramA, paramB);

                case MathOperationType.Multiplication:
                    return Number<T>.Calculator.Multiply(paramA, paramB);

                case MathOperationType.Division:
                    return Number<T>.Calculator.Divide(paramA, paramB);

                case MathOperationType.Average:
                    return Number<T>.Calculator.Divide(paramA, Convert.ToInt32(paramB));

                case MathOperationType.Perecentage:
                    return Number<T>.Calculator.Divide(paramA, Convert.ToInt32(paramB));

                default: return default(T);
            }
        }

        /// <summary>
        /// calculate for list of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operationType"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Calculate<T>(MathOperationType operationType, List<T> list)
        {
            switch (operationType)
            {
                case MathOperationType.Addition:
                    return new SummableList<T>(list).Sum();

                case MathOperationType.Sustraction:
                    return new SummableList<T>(list).Average();

                default: return default(T);
            }
        }
    }

    /// <summary>
    /// Provide all possible simple mathematical operation types
    /// </summary>
    public enum MathOperationType
    {
        Addition = 1,
        Sustraction,
        Division,
        Multiplication,
        Difference,
        Perecentage,
        Sum,
        Average,
        Mean,
        Median,
        Mode,
        StandardDiviation
    }

    /// <summary>
    /// This interface defines all of the operations that can be done in generic classes
    /// These operations can be assigned to operators in class Number<T>
    /// </summary>
    /// <typeparam name="T">Type that
    /// we will be doing arithmetic with</typeparam>
    public interface ICalculator<T>
    {
        T Add(T a, T b);
        T Sub(T a, T b);
        T Difference(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
        T Divide(T a, int b);
        //for doing integer division which is needed to do averages
    }

    /// <summary>
    /// This class uses reflection to automatically create the correct 
    /// ICalculator<T> that is needed for any particular type T.
    /// </summary>
    /// <typeparam name="T">Type that we
    /// will be doing arithmetic with</typeparam>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class Number<T>
    {
        private readonly T value;

        public Number(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Big IF chain to decide exactly which ICalculator needs to be created
        /// Since the ICalculator is cached, this if chain is executed only once per type
        /// </summary>
        /// <returns>The type of the calculator that needs to be created</returns>
        public static Type GetCalculatorType()
        {
            Type tType = typeof(T);
            Type calculatorType = null;
            if (tType == typeof(Int32))
            {
                calculatorType = typeof(Int32Calculator);
            }
            else if (tType == typeof(Int64))
            {
                calculatorType = typeof(Int64Calculator);
            }
            else if (tType == typeof(Double))
            {
                calculatorType = typeof(DoubleCalculator);
            }
            else if (tType == typeof(object))
            {
                calculatorType = typeof(ObjectCalculator);
            }
            else
            {
                throw new InvalidCastException(String.Format("Unsupported Type- Type {0}" +
                      " does not have a partner implementation of interface " +
                      "ICalculator<T> and cannot be used in generic " +
                      "arithmetic using type Number<T>", tType.Name));
            }
            return calculatorType;
        }

        /// <summary>
        /// a static field to store the calculator after it is created
        /// this is the caching that is refered to above
        /// </summary>
        private static ICalculator<T> fCalculator = null;

        /// <summary>
        /// Singleton pattern- only one calculator created per type
        /// </summary>
        public static ICalculator<T> Calculator
        {
            get
            {
                if (fCalculator == null)
                {
                    MakeCalculator();
                }
                return fCalculator;
            }
        }

        /// <summary>
        /// Here the actual calculator is created using the system activator
        /// </summary>
        public static void MakeCalculator()
        {
            Type calculatorType = GetCalculatorType();
            fCalculator = Activator.CreateInstance(calculatorType) as ICalculator<T>;
        }

        /// These methods can be called by the applications
        /// programmer if no operator overload is defined
        /// If an operator overload is defined these methods are not needed
        #region operation methods

        public static T Add(T a, T b)
        {
            return Calculator.Add(a, b);
        }

        public static T Sub(T a, T b)
        {
            return Calculator.Sub(a, b);
        }

        public static T Difference(T a, T b)
        {
            return Calculator.Difference(a, b);
        }

        public static T Multiply(T a, T b)
        {
            return Calculator.Multiply(a, b);
        }

        public static T Divide(T a, T b)
        {
            return Calculator.Divide(a, b);
        }

        public static T Divide(T a, int b)
        {
            return Calculator.Divide(a, b);
        }

        #endregion

        /// These operator overloads make doing the arithmetic easy.
        /// For custom operations, an operation method
        /// may be the only way to perform the operation
        #region Operators

        //IMPORTANT:  The implicit operators
        //allows an object of type Number<T> to be
        //easily and seamlessly wrap an object of type T. 
        public static implicit operator Number<T>(T a)
        {
            return new Number<T>(a);
        }

        //IMPORTANT:  The implicit operators allows 
        //an object of type Number<T> to be
        //easily and seamlessly wrap an object of type T. 
        public static implicit operator T(Number<T> a)
        {
            return a.value;
        }

        public static Number<T> operator +(Number<T> a, Number<T> b)
        {
            return Calculator.Add(a.value, b.value);
        }

        public static Number<T> operator -(Number<T> a, Number<T> b)
        {
            return Calculator.Difference(a, b);
        }

        public static Number<T> operator *(Number<T> a, Number<T> b)
        {
            return Calculator.Multiply(a, b);
        }

        public static Number<T> operator /(Number<T> a, Number<T> b)
        {
            return Calculator.Divide(a, b);
        }

        public static Number<T> operator /(Number<T> a, int b)
        {
            return Calculator.Divide(a, b);
        }
        #endregion
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SummableList<T> : List<T>
    {
        public SummableList(List<T> list)
        {
            AddRange(list);
        }

        public T Sum()
        {
            Number<T> result = default(T);

            foreach (T item in this)
                result += item;

            return result;
        }

        public T Average()
        {
            Number<T> sum = Sum();
            return sum / Count;
        }
    }

    /// <summary>
    /// ICalculator<T> implementation for Int32 type
    /// </summary>
    struct Int32Calculator : ICalculator<Int32>
    {
        public Int32 Add(Int32 a, Int32 b)
        {
            return a + b;
        }

        public Int32 Sub(Int32 a, Int32 b)
        {
            return a - b;
        }

        public Int32 Difference(Int32 a, Int32 b)
        {
            return a - b;
        }

        public int Multiply(Int32 a, Int32 b)
        {
            return a * b;
        }

        public int Divide(Int32 a, Int32 b)
        {
            return a / b;
        }
    }

    /// <summary>
    /// ICalculator<T> implementation for Int64 type
    /// </summary>
    struct Int64Calculator : ICalculator<long>
    {
        public long Add(long a, long b)
        {
            return a + b;
        }

        public long Sub(long a, long b)
        {
            return a - b;
        }

        public long Difference(long a, long b)
        {
            return a - b;
        }

        public long Multiply(long a, long b)
        {
            return a * b;
        }

        public long Divide(long a, long b)
        {
            return a / b;
        }

        public long Divide(long a, int b)
        {
            return a / b;
        }
    }

    /// <summary>
    /// ICalculator<T> implementation for Double type
    /// </summary>
    struct DoubleCalculator : ICalculator<Double>
    {
        public Double Add(Double a, Double b)
        {
            return a + b;
        }

        public Double Sub(Double a, Double b)
        {
            return a - b;
        }

        public Double Difference(Double a, Double b)
        {
            return a - b;
        }

        public Double Multiply(Double a, Double b)
        {
            return a * b;
        }

        public Double Divide(Double a, Double b)
        {
            return a / b;
        }

        public Double Divide(Double a, int b)
        {
            return a / b;
        }
    }

    /// <summary>
    /// ICalculator<T> implementation for object type
    /// Objects will be considered as double value
    /// </summary>
    struct ObjectCalculator : ICalculator<object>
    {
        public object Add(object a, object b)
        {
            return Convert.ToDouble(a) + Convert.ToDouble(b);
        }

        public object Sub(object a, object b)
        {
            return Convert.ToDouble(a) - Convert.ToDouble(b);
        }

        public object Difference(object a, object b)
        {
            return Convert.ToDouble(a) - Convert.ToDouble(b);
        }

        public object Multiply(object a, object b)
        {
            return Convert.ToDouble(a) * Convert.ToDouble(b);
        }

        public object Divide(object a, object b)
        {
            return Convert.ToDouble(a) / Convert.ToDouble(b);
        }

        public object Divide(object a, int b)
        {
            return Convert.ToDouble(a) / b;
        }
    }
}
