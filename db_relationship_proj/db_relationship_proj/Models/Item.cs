using System.ComponentModel.DataAnnotations.Schema;

namespace db_relationship_proj.Models
{
    public class Item
    {
        
            public int Id { get; set; }
            public string Name { get; set; } = null;
            public decimal Price { get; set; }
            public int? SerialNumberId { get; set; }
            public SerialNumber? SerialNumber { get; set; }
            public int? CategoryId { get; set; }
            [ForeignKey("CategoryId")]
            public Category? category { get; set; }

        public List<ItemClient>? ItemClients {  get; set; }

    }
}
