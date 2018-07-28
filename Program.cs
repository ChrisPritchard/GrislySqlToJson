using System;
using System.Linq;
using System.Collections.Generic;
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
            
            using(var context = new GrislyGrottoDbContext(connString))
            using(var zipFile = new FileStream($"./allposts-{DateTime.Now.ToString("dd-MM-yyyy")}.zip", FileMode.Create))
            using(var archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
            {
                var index = new List<(string,string)>();
                var posts = context.Posts.Include(o => o.Author).Include(o => o.Comments).ToArray();
                foreach(var post in posts)
                {
                    var entry = archive.CreateEntry(post.Key + ".json");
                    using(var writer = new StreamWriter(entry.Open()))
                        writer.Write(JsonConvert.SerializeObject(post));

                    index.Add((post.Date.ToString("s"), post.Key));
                }

                var indexEntry = archive.CreateEntry("index.txt");
                using(var writer = new StreamWriter(indexEntry.Open()))
                    index.OrderByDescending(o => o).ToList().ForEach(o => {
                        var (date, key) = o;
                        writer.WriteLine(date + "\t" + key);
                    });
            }
        }
    }
}
