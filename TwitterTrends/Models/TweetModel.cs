using System.Text.RegularExpressions;
using TwitterTrends.Parsers;

namespace TwitterTrends.Models
{
    public class TweetModel 
    {
        private CoordinateModel _coords;
        private float _sentiment; 

        public TweetModel(string message, CoordinateModel coords) 
        {
            _coords = coords;
            
            Regex wordRegex = new Regex(@"[a-zA-Z]+"); //Регулярка на последовательность букв 
            MatchCollection matches = wordRegex.Matches(message); //Заносим по этой регулярке результаты(все слова message)

            float sentiment = 0; 
            foreach (Match match in matches) //Проходимся по всем словам
            {
                if (MainWindow.KeyWords.TryGetValue(match.Value, out float sent)) //Если в словаре "слово->порядочность" есть такое слово
                {
                    sentiment += sent; //то добовляем его порядочность
                }
            }

            _sentiment = sentiment; 
        }

        public CoordinateModel Coords
        {
            get => _coords;
            set => _coords = value;
        }

        public float Sentiment
        {
            get => _sentiment;
            set => _sentiment = value;
        }
    }
}