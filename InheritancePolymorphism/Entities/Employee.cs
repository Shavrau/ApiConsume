namespace InheritancePolymorphism.Entities
{
    public class Employee
    {
        public string Name;
        public int Hours;
        public double ValuePerHour;

        public Employee(string name, int hours, double valuePerHour)
        {
            Name = name;
            Hours = hours;
            ValuePerHour = valuePerHour;
        }

        public virtual double Payment()
        {
            return Hours * ValuePerHour;
        }
    }
}
