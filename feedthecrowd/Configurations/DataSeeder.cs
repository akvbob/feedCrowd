using FeedTheCrowd.Data;
using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Configurations
{
    public class DataSeeder
    {
        const string fileName = @"D:\7 semestras\Saitynai\data.txt";
        public static void Seed(FeedTheCrowdContext _context)
        {
            string line;
            int count = 1;
            
            if (!_context.Recipe.Any() || !_context.Product.Any())
            {

                using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        SeedTheDatabase(line, _context, count);
                        count++;
                    }
                }
            }
        }
        public static void SeedTheDatabase(string line, FeedTheCrowdContext _context, int count)
        {
            string[] mas = line.Split(';');
            string ing = mas[5];
            string qua = mas[6];
            string[] ings = ing.Split(':').Where(x => !string.IsNullOrEmpty(x)).ToArray(); 
            string[] quas = qua.Split(':').Where(x => !string.IsNullOrEmpty(x)).ToArray(); 
            var recipe = new Recipe { Title = mas[1], Time = mas[0], Image = mas[2], Description = mas[4], Servings = Int32.Parse(mas[3]), };
                _context.Add(recipe);
                _context.SaveChanges();

            for (int i = 0; i < ings.Length; i++)
            {
                var product = new Product { Name = ings[i], Quantity = Double.Parse(quas[i]), FkRecipe = count };
                _context.Add(product);
            }
            _context.SaveChanges();


        }

    }
    
}
