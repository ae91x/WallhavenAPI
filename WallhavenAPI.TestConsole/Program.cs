using System;

namespace WallhavenAPI.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = new Structs.Query();
            query.Resolutions = new string[] { "1920x1080" };
            query.ExactResolution = true;
            query.Purities = Structs.Query.Purity.NSFW;
            query.Sort = Structs.Query.SortMethod.Favorites;

            var test = API.Search(query);

            Console.WriteLine(test[0]);
            int x = 0;
        }
    }
}
