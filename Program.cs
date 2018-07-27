using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GrislyGrotto;
using System.IO;
using Newtonsoft.Json;
using System.IO.Compression;

namespace GrislySqlToJson
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = args[0];
            var context = new GrislyGrottoDbContext(connString);
            var posts = context.Posts.Include(o => o.Comments).ToArray();

            using(var zipFile = new FileStream($"./allposts-{DateTime.Now.ToString("dd-MM-yyyy")}.zip", FileMode.Create))
            using(var archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
            {
                foreach(var post in posts)
                {
                    var entry = archive.CreateEntry(post.Key + ".json");
                    using(var writer = new StreamWriter(entry.Open()))
                        writer.Write(JsonConvert.SerializeObject(post));
                }
            }
        }
    }
}
