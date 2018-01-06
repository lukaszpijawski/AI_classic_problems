using System.Collections.Generic;

namespace Przesuwanka
{
    public class Neighbour
    {
        public City City { get; }
        public double Distance { get; private set; }       
        
        public Neighbour(City city, double distance)
        {
            City = city;
            Distance = distance;
        }

        public string Name
        {
            get
            {
                return City.Name;
            }
        }

        public Point Coordinates
        {
            get
            {
                return City.Coordinates;
            }
        }

        public List<Neighbour> Neighbours
        {
            get
            {
                return City.Neighbours;
            }
        }
    }
}
