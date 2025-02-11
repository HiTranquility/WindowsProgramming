using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1.Functions
{
    public class ComplexNumber
    {

        #region-- Properties --
        private int realPart;
        private int imaginaryPart;
        #endregion
        #region -- Gets;Sets --
        public int RealPart
        {
            get { return realPart; }
            set { this.realPart = value; }
        }
        public int ImaginaryPart
        {
            get { return imaginaryPart; }
            set { this.imaginaryPart = value; }
        }
        #endregion
        #region -- Constructors --
        public ComplexNumber() { }
        public ComplexNumber(int realPart, int imaginaryPart)
        {
            this.realPart = realPart;
            this.imaginaryPart = imaginaryPart;
        }
        #endregion
        #region-- Methods --
        // Addition
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.RealPart + b.RealPart, a.ImaginaryPart + b.ImaginaryPart);
        }

        public static ComplexNumber Add(ComplexNumber a, ComplexNumber b)
        {
            return a + b;
        }

        // Subtraction
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.RealPart - b.RealPart, a.ImaginaryPart - b.ImaginaryPart);
        }

        public static ComplexNumber Subtract(ComplexNumber a, ComplexNumber b)
        {
            return a - b;
        }

        // Multiplication
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                a.RealPart * b.RealPart - a.ImaginaryPart * b.ImaginaryPart,
                a.RealPart * b.ImaginaryPart + a.ImaginaryPart * b.RealPart
            );
        }

        public static ComplexNumber Multiply(ComplexNumber a, ComplexNumber b)
        {
            return a * b;
        }

        // Division
        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double denominator = b.RealPart * b.RealPart + b.ImaginaryPart * b.ImaginaryPart;
            return new ComplexNumber(
                (int)((a.RealPart * b.RealPart + a.ImaginaryPart * b.ImaginaryPart) / denominator),
                (int)((a.ImaginaryPart * b.RealPart - a.RealPart * b.ImaginaryPart) / denominator)
            );
        }

        public static ComplexNumber operator %(ComplexNumber a, ComplexNumber b)
        {
            double denominator = b.RealPart * b.RealPart + b.ImaginaryPart * b.ImaginaryPart;
            if (denominator == 0)
                throw new DivideByZeroException("Cannot divide by zero in complex number division.");
            return new ComplexNumber(
                (int)((a.RealPart * b.RealPart + a.ImaginaryPart * b.ImaginaryPart) / denominator),
                (int)((a.ImaginaryPart * b.RealPart - a.RealPart * b.ImaginaryPart) / denominator)
            );
        }

        public static ComplexNumber Divide(ComplexNumber a, ComplexNumber b)
        {
            return a / b;
        }
        public override string ToString()
        {
            if (imaginaryPart == 0)
                return $"{realPart}";
            if (realPart == 0)
                return $"{imaginaryPart}i";
            if (imaginaryPart > 0)
                return $"{realPart} + {imaginaryPart}i";
            else
                return $"{realPart} - {-imaginaryPart}i";
        }

        // ToString method
        #endregion
    }
}
