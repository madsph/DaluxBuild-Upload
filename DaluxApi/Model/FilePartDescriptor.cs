namespace DaluxApi.Model
{
    public class FilePartDescriptor
    {
        public int Start { get; set; }
        public int End { get; set; }
        public long Total { get; set; }

        public override string ToString()
        {
            return $"{Start}-{End}/{Total}";
        }
    }
}