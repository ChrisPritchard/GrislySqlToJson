using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GrislyGrotto;
using System.IO;
using Newtonsoft.Json;

namespace GrislySqlToJson
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = args[0];
            var context = new GrislyGrottoDbContext(connString);

            var posts = context.Posts.Take(3).ToArray();
            foreach(var post in posts)
                File.WriteAllText(post.Key + ".json", JsonConvert.SerializeObject(post));
        }
    }
}
