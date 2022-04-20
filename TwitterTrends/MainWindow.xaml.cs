using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using TwitterTrends.Models;
using TwitterTrends.Parsers;


namespace TwitterTrends
{
    public partial class MainWindow
    {
        public static Dictionary<string, float> KeyWords;
        public MainWindow()
        {
            InitializeComponent();
            KeyWords = KeyWordsParser.Parse("../../Resources/sentiments.csv"); //Считываем словарь "порядочности" слов
            List<StateModel> states = StateParser.Parse("../../Resources/states.json"); //Считываем список штатов
            List <TweetModel> tweets = TweetParser.Parse("../../Resources/Tweets/cali_tweets2014.txt"); //Считывем вот эти твиты
            InitStates(states, tweets);                                      //так же при необходимости меняем файл с твитами
            DrawMap(states);
        }
        

        private void InitStates(List<StateModel> states, List<TweetModel> tweets) //Соотнести твиты со штатами по кординатам
        {
            foreach (TweetModel tweet in tweets) //Проходимся по всем твитам
            {
                bool wasFound = false;
                foreach (StateModel state in states) //Проходимся по все штатам
                {
                    foreach (PolygonModel polygon in state.Polygons) //Проходим по всем полигонам этого штата
                    {
                        if (tweet.Coords.IsPointInPolygon(polygon)) //Если коржинаты этого твита находятся в этом полигоне
                        {
                            state.Tweets.Add(tweet); //Добовляем этому штату данный твит
                            wasFound = true; //Помечаем что всё прошло хорошо
                            break; //выходим из цикла полигонов
                        }
                    }
                    if (wasFound) //Если уже был найден штат для данного твита 
                        break; //Выходим из цикла штатов
                }
            }
            
        }
        private void DrawMap(List<StateModel> states) //Нарисовать карту на экран
        {
            foreach (StateModel state in states)
            {
                foreach (PolygonModel polygon in state.Polygons)
                {
                    DrawPolygon(polygon, state.CalcColor());
                }
            }
        }
        private void DrawPolygon(PolygonModel polygon, Color color) {

            SolidColorBrush sentimentBrush = new SolidColorBrush(color);
            SolidColorBrush borderBrush = new SolidColorBrush()
            {
                Color = Colors.Black
            };  
            
            Polygon polygonView = new Polygon()
            {
                Stroke = borderBrush,
                Fill = sentimentBrush,
                StrokeThickness = 1
            };  
            
            PointCollection polygonPoints = new PointCollection();  
            foreach (CoordinateModel coords in polygon.Coords)
            {
                polygonPoints.Add(new System.Windows.Point(Math.Abs(coords.X) * 12 - 500, coords.Y * 12 - 100));
            } 
            polygonView.Points = polygonPoints;
            
            LayoutRoot.Children.Add(polygonView);  
        }  
    }
}