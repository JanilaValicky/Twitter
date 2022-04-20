namespace TwitterTrends.Converters
{
    public interface IConverter<TA, TB> //интерфес поддверживающий конвертацию из одного типа в дрогой и обратно
    {
        TB DoForward(TA ta);
        TA DoBackward(TB tb);
    }
}