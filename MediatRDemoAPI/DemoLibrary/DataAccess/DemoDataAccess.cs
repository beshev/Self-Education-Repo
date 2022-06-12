namespace DemoLibrary.DataAccess
{
    using DemoLibrary.Models;

    public class DemoDataAccess : IDataAccess
    {
        private readonly List<PersonModel> _people = new();

        public DemoDataAccess()
        {
            _people.Add(new PersonModel(1, "Tomy", "Lee"));
            _people.Add(new PersonModel(2, "Merry", "Popins"));
        }

        public PersonModel DeletePerson(int id)
        {
            var person = _people.FirstOrDefault(x => x.Id == id);

            _people.Remove(person);

            return person;
        }

        public IEnumerable<PersonModel> GetPeople()
        {
            return _people;
        }

        public PersonModel GetPerson(int id)
        {
            return _people.FirstOrDefault(x => x.Id == id);
        }

        public PersonModel InsertPerson(string firstName, string lastName)
        {
            var id = _people.Max(x => x.Id) + 1;
            var person = new PersonModel(id, firstName, lastName);
            _people.Add(person);

            return person;
        }
    }
}
