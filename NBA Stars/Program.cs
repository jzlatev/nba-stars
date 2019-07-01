using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NBA_Stars
{
    class Program
    {
        static void Main(string[] args)
        {
            int currentYear = (int)DateTime.Now.Year;

            string path = args[0];
            string outputPath;
            int rating;
            int requiredExperience;
            string input;
            if (!File.Exists(path))
            {
                throw new Exception("Missing file");
            }

            Console.Write("The maximum number of years the player has played in the league to qualify: ");
            input = Console.ReadLine();
            Int32.TryParse(input, out requiredExperience);

            Console.Write("The minimum rating the player should have to qualify: ");
            input = Console.ReadLine();
            Int32.TryParse(input, out rating);

            Console.Write("Path to the generated CSV file: ");

            outputPath = Console.ReadLine();
            
            string jsonString = System.IO.File.ReadAllText(path);

            List<Player> list = new List<Player>();
            JArray jsonArray = JArray.Parse(jsonString);
            foreach (JObject obj in jsonArray.Children<JObject>())
            {                Player player = new Player();
                player.loadFromArray(obj);
                if (player.hasMoreRatingThan(rating) && player.hasMoreExperienceThan(requiredExperience,currentYear))
                {
                    list.Add(player);
                }
                                 
            }

            list.Sort();
            list.Reverse();

            var csv = new StringBuilder();
            csv.AppendLine(string.Format("Name,Rating"));
            list.ForEach((player)=>csv.AppendLine(string.Format("{0}, {1}",player.getName(),player.getRating())));

            File.WriteAllText(outputPath, csv.ToString());
        }
    }
}
