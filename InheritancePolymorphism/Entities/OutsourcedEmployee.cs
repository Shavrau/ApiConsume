namespace InheritancePolymorphism.Entities
{
    public class OutsourcedEmployee : Employee
    {
        public double AdditionalCharge;

        public OutsourcedEmployee(string name, int hours, double valuePerHour, double additionalCharge)
            : base(name, hours, valuePerHour)
        {
            AdditionalCharge = additionalCharge;
        }

        public override double Payment()
        {
            return base.Payment() * 1.16 + AdditionalCharge;
        }
    }
}
