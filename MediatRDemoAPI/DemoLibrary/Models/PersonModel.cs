namespace DemoLibrary.Models
{
    public class PersonModel
    {
        public PersonModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; init; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
