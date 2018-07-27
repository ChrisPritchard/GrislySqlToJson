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

            var outDir = "./out";
            if(Directory.Exists(outDir))
                Directory.Delete(outDir, true);
            Directory.CreateDirectory(outDir);

            var posts = context.Posts.Include(o => o.Comments).Take(100).ToArray();
            foreach(var post in posts)
                File.WriteAllText($"{outDir}/{post.Key}.json", JsonConvert.SerializeObject(post));
        }
    }
}
