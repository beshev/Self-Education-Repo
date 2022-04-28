namespace MongoDBDemo
{
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDataAccess.DataAccess;
    using MongoDataAccess.Models;

    public class Program
    {
        static async Task Main()
        {
            ChoreDataAccess db = new ChoreDataAccess();

            var user = new UserModel
            {
                FirstName = "Joe",
                LastName = "Jones",
            };

            await db.CreateUserAsync(user);

            var users = await db.GetAllUsersAsync();

            var chore = new ChoreModel
            {
                AssignedTo = users.FirstOrDefault(),
                ChoreText = "Mow the Lawn",
                FrequencyInDays = 7,
            };

            await db.CreateChoreAsync(chore);

            var chores = await db.GetAllChoresAsync();

            var newChore = chores.First();

            await db.CompleteChore(newChore);
        }
    }
}
