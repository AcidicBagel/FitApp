namespace FitApp.Domain.Models
{
    public class Membership
    {
        public string Name { get; }
        public uint Price { get; }
        public string Description { get; }

        public Membership(
            string name, 
            uint price, 
            string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }
    }
}