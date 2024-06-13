using System;
using System.Collections.Generic;
using System.Globalization;
using InheritancePolymorphism.Entities;
using InheritancePolymorphism.Services;

namespace InheritancePolymorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = ReadEmployeesFromConsole();

            FileService fileService = new FileService();
            string directoryPath = @"C:\Programmers\InheritancePolymorphism\csvs";
            fileService.WriteToCsv(employees, directoryPath);

            Console.WriteLine($"Data written to {directoryPath}\\employees.csv successfully.");
        }

        static List<Employee> ReadEmployeesFromConsole()
        {
            List<Employee> employees = new List<Employee>();

            Console.Write("Enter the number of employees: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"Employee #{i} data:");
                Console.Write("Outsourced (y/n)? ");
                char ch = char.Parse(Console.ReadLine());

                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Hours: ");
                int hours = int.Parse(Console.ReadLine());
                Console.Write("Value per hour: ");
                double valuePerHour = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                if (ch == 'y' || ch == 'Y')
                {
                    Console.Write("Additional charge: ");
                    double additionalCharge = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    employees.Add(new OutsourcedEmployee(name, hours, valuePerHour, additionalCharge));
                }
                else
                {
                    employees.Add(new Employee(name, hours, valuePerHour));
                }
            }

            return employees;
        }
    }
}
