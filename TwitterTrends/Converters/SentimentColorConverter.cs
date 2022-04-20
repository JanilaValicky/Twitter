using System;
using System.Windows.Media;

namespace TwitterTrends.Converters
{
    public class SentimentColorConverter : IConverter<float, Color> //Класс установщика цвета, включает в себя конвертацию дробного чила в цвет и обратно
    {
        public Color DoForward(float averageSentiment) //Из число с плавающей точкой в цвет
        {
            if (averageSentiment>0) //Если оценка штата позитивна
                //То красим по этой формуле
                return Color.FromRgb((byte)(255-120*(1-averageSentiment)),(byte)(255-130*(1-averageSentiment)), (byte)(100*(1-averageSentiment)*(1-averageSentiment)));
            //Иначе по этой
            return Color.FromRgb((byte)(110*(1+averageSentiment)),(byte)(120*(1+averageSentiment)), (byte)(255-100*(1+averageSentiment)));
        }

        public float DoBackward(Color color) //Обратной конвертации нет но исходя из интерфейса мы должны реализовать этот метод
        {
            throw new NotImplementedException(); //Затычка вызывающая исключение при вызове
        }
    }
}