namespace TwitterTrends.Models
{
    public class CoordinateModel
    {
      
        private readonly double _x;
        private readonly double _y;

        public CoordinateModel(double x, double y) 
        {
            _x = x;
            _y = y;
        }

        public double X => _x; 

        public double Y => _y; 
        
        public bool IsPointInPolygon(PolygonModel polygon) //Функция проверяющая находится ли данная точка внутри переданного полигона
        {

            bool result = false;
            int j = polygon.Coords.Count - 1;
            for (int i = 0; i < polygon.Coords.Count; i++)
            {
                if (polygon.Coords[i].Y < Y && polygon.Coords[j].Y >= Y || polygon.Coords[j].Y < Y && polygon.Coords[i].Y >= Y)
                {
                    if (polygon.Coords[i].X + (Y - polygon.Coords[i].Y) / (polygon.Coords[j].Y - polygon.Coords[i].Y) * (polygon.Coords[j].X - polygon.Coords[i].X) < X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
    }
}