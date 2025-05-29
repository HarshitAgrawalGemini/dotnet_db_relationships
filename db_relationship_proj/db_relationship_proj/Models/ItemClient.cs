namespace db_relationship_proj.Models
{
    public class ItemClient
    {

        public int ItemId { get; set; }
        public Item item { get; set; } 
        public int ClientId { get; set; }
        public Client client { get; set; } 


    }
}
