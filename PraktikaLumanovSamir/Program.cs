using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursowaya_Lumanov
{
    class Polynomial
    {
        private List<double> coefficients;

        public Polynomial(List<double> coeffs)
        {
            coefficients = new List<double>(coeffs);
        }

        // Методы ввода-вывода
        public void Input()
        {
            Console.WriteLine("Введите коэффициенты через пробел:");
            string input = Console.ReadLine();
            string[] coeffs = input.Trim().Split(' ');
            coefficients = new List<double>();
            foreach (string coeff in coeffs)
            {
                double coefficient;
                if (!double.TryParse(coeff, out coefficient))
                {
                    Console.WriteLine("Ошибка: Не удалось преобразовать в число.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    return;
                }
                coefficients.Add(coefficient);
            }
        }

        public void Output(string title)
        {
            Console.WriteLine(title + ":");
            for (int i = 0; i < coefficients.Count; i++)
            {
                Console.Write(coefficients[i] + "x^" + i + " ");
            }
            Console.WriteLine();
        }

        // Методы сложения и умножения полиномов
        public Polynomial Add(Polynomial other)
        {
            List<double> resultCoeffs = new List<double>();
            int maxLength = Math.Max(coefficients.Count, other.coefficients.Count);
            for (int i = 0; i < maxLength; i++)
            {
                double coeff1 = (i < coefficients.Count) ? coefficients[i] : 0;
                double coeff2 = (i < other.coefficients.Count) ? other.coefficients[i] : 0;
                resultCoeffs.Add(coeff1 + coeff2);
            }
            return new Polynomial(resultCoeffs);
        }

        public Polynomial Multiply(Polynomial other)
        {
            List<double> resultCoeffs = new List<double>(new double[coefficients.Count + other.coefficients.Count - 1]);
            for (int i = 0; i < coefficients.Count; i++)
            {
                for (int j = 0; j < other.coefficients.Count; j++)
                {
                    resultCoeffs[i + j] += coefficients[i] * other.coefficients[j];
                }
            }
            return new Polynomial(resultCoeffs);
        }

        // Метод умножения полинома на число
        public Polynomial Multiply(double scalar)
        {
            List<double> resultCoeffs = new List<double>();
            foreach (double coeff in coefficients)
            {
                resultCoeffs.Add(coeff * scalar);
            }
            return new Polynomial(resultCoeffs);
        }

        // Методы интегрирования и дифференцирования
        public Polynomial Integrate()
        {
            List<double> resultCoeffs = new List<double>();
            for (int i = 0; i < coefficients.Count; i++)
            {
                resultCoeffs.Add(coefficients[i] / (i + 1));
            }
            resultCoeffs.Insert(0, 0); // Добавляем свободный член
            return new Polynomial(resultCoeffs);
        }

        public Polynomial Differentiate()
        {
            List<double> resultCoeffs = new List<double>();
            for (int i = 1; i < coefficients.Count; i++)
            {
                resultCoeffs.Add(coefficients[i] * i);
            }
            return new Polynomial(resultCoeffs);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Polynomial poly1 = new Polynomial(new List<double>());
            Polynomial poly2 = new Polynomial(new List<double>());

            Console.WriteLine("Введите коэффициенты для первого полинома:");
            poly1.Input();
            Console.WriteLine("Введите коэффициенты для второго полинома:");
            poly2.Input();

            Console.WriteLine();

            poly1.Output("Первый полином:");
            poly2.Output("Второй полином:");

            Console.WriteLine();

            Polynomial sum = poly1.Add(poly2);
            Polynomial product = poly1.Multiply(poly2);

            sum.Output("Сумма полиномов");

            product.Output("Произведение полиномов");

            Console.WriteLine();

            Console.WriteLine("Введите число, на которое хотите умножить первый полином:");
            double scalar;
            while (!double.TryParse(Console.ReadLine(), out scalar))
            {
                Console.WriteLine("Ошибка: Не удалось преобразовать в число. Попробуйте еще раз:");
            }

            Polynomial multipliedByScalar = poly1.Multiply(scalar);
            multipliedByScalar.Output("Полином, умноженный на число");

            Console.WriteLine();

            Polynomial integral = poly1.Integrate();
            integral.Output("Интеграл первого полинома");

            Polynomial derivative = poly1.Differentiate();
            derivative.Output("Производная первого полинома");

            Console.WriteLine();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }

}
