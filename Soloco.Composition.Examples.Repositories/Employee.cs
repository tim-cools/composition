namespace Soloco.Composition.Example.Repositories
{
    public class Employee
    {
        public virtual int Id { get; private set; }
        public virtual string CountryCode { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Store Store { get; set; }
    }
}