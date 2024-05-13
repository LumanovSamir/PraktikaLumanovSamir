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
        public void Input() //При неправильном вводе значений программа вылетает с ошибкой
        {
            Console.WriteLine("Вводите коэффиценты через пробел"); //Если ввести пробел в конце выводится ошибка
            string input = Console.ReadLine();
            string[] coeffs = input.Split(' ');
            coefficients = new List<double>();
            foreach (string coeff in coeffs)
            {
                coefficients.Add(double.Parse(coeff));
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

            poly1.Output("Первый полином");
            poly2.Output("Второй полином");

            Console.WriteLine();

            Polynomial sum = poly1.Add(poly2);
            Polynomial product = poly1.Multiply(poly2);
            Polynomial multipliedByScalar = poly1.Multiply(2);
            Polynomial integral = poly1.Integrate();
            Polynomial derivative = poly1.Differentiate();

            sum.Output("Сумма полиномов");
            product.Output("Произведение полиномов");
            multipliedByScalar.Output("Полином, умноженный на 2"); //пользователь не может изменить число без редактирования кода
            integral.Output("Интеграл полинома");
            derivative.Output("Производная полинома");

            Console.WriteLine();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}