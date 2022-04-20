using System.Collections.Generic;

namespace TwitterTrends.Models
{
    public class PolygonModel //Класс полигон (по факту множество некоторых точек соединённые линией)
    {
        private readonly List<CoordinateModel> _coords; //Связанный список точен приватный

        public PolygonModel(List<CoordinateModel> coords) 
        {
            _coords = coords;
        }

        public List<CoordinateModel> Coords => _coords;
    }
}