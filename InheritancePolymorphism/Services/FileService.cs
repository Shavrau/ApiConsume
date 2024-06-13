using System.Collections.Generic;
using System.Globalization;
using System.IO;
using InheritancePolymorphism.Entities;

namespace InheritancePolymorphism.Services
{
    public class FileService
    {
        public void WriteToCsv(List<Employee> employees, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, "employees.csv");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Name,TotalPayment");

                    foreach (Employee emp in employees)
                    {
                        string name = emp.Name;
                        string totalPayment = emp.Payment().ToString("F2", CultureInfo.InvariantCulture);
                        writer.WriteLine($"{name},{totalPayment}");
                    }

                    writer.Flush();
                }
            }
            catch (IOException e)
            {
                throw new IOException($"An error occurred while writing to the file: {e.Message}");
            }
        }
    }
}
