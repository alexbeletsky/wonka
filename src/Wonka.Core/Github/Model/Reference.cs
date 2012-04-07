namespace Wonka.Core.Github.Model
{
    public class Reference
    {
        public string Ref { get; set; }
        public string Url { get; set; }
        public Object Object { get; set; }
    }

    public class Object
    {
        public string Type { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
    }
}