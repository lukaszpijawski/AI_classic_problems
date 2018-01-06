using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class City
    {
        public string Name { get; private set; }
        public List<Neighbour> Neighbours { get; private set; }
        public Point Coordinates { get; private set; }

        public City(string name, Point coordinates)
        {
            Name = name;
            Coordinates = coordinates;
            Neighbours = new List<Neighbour>();            
        }

        public void AddNeighbour(City city, double distance)
        {
            var newNeighbour = new Neighbour(city, distance);
            if (!Neighbours.Exists(a => a.Name == newNeighbour.Name))
            {
                Neighbours.Add(newNeighbour);
            }
            if (!city.Neighbours.Exists(a => a.Name == this.Name))
            {
                city.AddNeighbour(this, distance);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class CityWithPathOfCities
    {
        public City City { get; set; }
        public List<City> VisitedCities { get; set; }

        public CityWithPathOfCities(City city)
        {
            City = city;
            VisitedCities = new List<City>();
        }

        public static implicit operator City(CityWithPathOfCities cityWithPathOfCities)
        {
            return cityWithPathOfCities.City;
        }

        public string Name
        {
            get
            {
                return City.Name;
            }
        }

        public List<Neighbour> Neighbours
        {
            get
            {
                return City.Neighbours;
            }
        }

        public Point Coordinates
        {
            get
            {
                return City.Coordinates;
            }
        }
    }
}
