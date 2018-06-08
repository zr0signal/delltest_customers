namespace DellTest.Customers.Service.Models
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}
