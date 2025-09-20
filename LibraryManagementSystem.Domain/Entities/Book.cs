namespace LibraryManagementSystem.Domain.Entities
{
    public class Book : BaseEntity
    {
        public Book()
        {
            BookCategories = new List<BookCategory>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
