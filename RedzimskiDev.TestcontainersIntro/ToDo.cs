namespace RedzimskiDev.TestcontainersIntro
{
    public class ToDo
    {
        public int Id { get; private set; }
        public string Content { get; private set; }

        public ToDo(string content)
        {
            Content = content;
        }

        public ToDo(int id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}