using System;
using System.Runtime.InteropServices;

namespace WallhavenAPI.TestConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            var query = new Structs.Query();
            query.Resolutions = new string[] { "1920x1080" };
            query.ExactResolution = true;
            query.Purities = Structs.Query.Purity.SFW;
            query.Sort = Structs.Query.SortMethod.Views;
            var test = API.Search(query);

            var rnd = new Random();
            var file = API.GetFile(test[rnd.Next(test.Count)]);

            System.IO.File.WriteAllBytes(@"F:\test.jpg", file);
        }
    }
}
