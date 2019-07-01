using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Stars
{
    class Player: IEquatable<Player>, IComparable<Player>
    {
        private string name;
        private int since;
        private string position;
        private int rating;
    
        public void loadFromArray(JObject obj)
        {
            name = (string)obj.Property("Name").Value;
            since = (int)obj.Property("PlayingSince").Value;
            position = (string)obj.Property("Position").Value;
            rating = (int)obj.Property("Rating").Value;
        }

        public bool hasMoreRatingThan(int rating)
        {
            if (this.rating >= rating)
            {
                return true;
            }
            return false;
        }

        public bool hasMoreExperienceThan(int years,int currentYear)
        {
            if (years<=currentYear-this.since)
            {
                return true;
            }
            return false;
        }

        public string getName()
        {
            return name;
        }
        public int getRating()
        {
            return rating;
        }

        int IComparable<Player>.CompareTo(Player other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.rating.CompareTo(other.getRating());
            }
        }

        public bool Equals(Player other)
        {
            return other.getRating() == this.rating;
        }
    }
}
