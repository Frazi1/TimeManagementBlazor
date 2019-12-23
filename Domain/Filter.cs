namespace Domain
{
    public class Filter
    {
        public int Skip { get; }
        public int Take { get; }

        public Filter(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}