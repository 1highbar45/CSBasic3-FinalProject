namespace FinalProject.WebMVC
{
    public class Demo : Animal, IFlyable
    {
        public Demo(string name) : base(name)
        {
        }

        public void Fly()
        {
            throw new NotImplementedException();
        }

        public override void MakeSound()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Animal
    {
        public string Name { get; set; }

        public Animal(string name)
        {
            Name = name;
        }

        public void Eat()
        {
            Console.WriteLine($"{Name} is eating.");
        }

        public abstract void MakeSound();
    }

    public interface IFlyable
    {
        void Fly();
    }
}
