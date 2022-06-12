namespace DemoLibrary.DataAccess
{
    using DemoLibrary.Models;

    public interface IDataAccess
    {
        public IEnumerable<PersonModel> GetPeople();

        public PersonModel GetPerson(int id);

        public PersonModel InsertPerson(string fFirstName, string lastName);

        public PersonModel DeletePerson(int id);
    }
}
