using Models.Data;

namespace Models
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using UniversContext context= new UniversContext();

            var result = context.Users.Select(x => x.FirstName);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}