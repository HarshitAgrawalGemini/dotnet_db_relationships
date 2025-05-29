namespace db_relationship_proj.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ItemClient> itemClients { get; set; }
    }
}
