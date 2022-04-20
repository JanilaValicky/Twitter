using System.Collections.Generic;
using System.Windows.Media;
using TwitterTrends.Converters;

namespace TwitterTrends.Models
{
    public class StateModel 
    {
        private string _name; 
        private List<PolygonModel> _polygons; 
        private List<TweetModel> _tweets = new List<TweetModel>(); 
        
        public StateModel(string name, List<PolygonModel> polygons) 
        {
            _name = name;
            _polygons = polygons;
        }
        
        public string Name 
        {
            get => _name; //
            set => _name = value;
        }

        public List<PolygonModel> Polygons
        {
            get => _polygons;
            set => _polygons = value;
        }

        public List<TweetModel> Tweets
        {
            get => _tweets;
            set => _tweets = value;
        }


        public Color CalcColor() 
        {
            if (_tweets == null || _tweets.Count == 0) return Colors.Gray; //Если нет твитов в этом штате, то возвращаем серый цвет
            float sentimentSum = 0; //Сумматор "порядочности"
            foreach (TweetModel tweet in _tweets) //Проходимся по всем твитам штата
            {
                sentimentSum += tweet.Sentiment; //Добовляя их "порядочность"
            }
            return new SentimentColorConverter().DoForward(sentimentSum / _tweets.Count); //Возвращаем сконвертированный цвет по среднему значению
        }
    }
}