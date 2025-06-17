namespace CRUD_API_Demo.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
