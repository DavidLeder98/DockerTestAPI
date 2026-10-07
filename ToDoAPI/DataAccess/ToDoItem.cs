namespace ToDoAPI.DataAccess
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
    }
}
