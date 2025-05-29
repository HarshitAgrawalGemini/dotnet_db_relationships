using System.ComponentModel.DataAnnotations.Schema;

namespace db_relationship_proj.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public  int? ItemId{ get; set; }
        [ForeignKey("ItemId")]
        // this means ItemId will be the foreigb key 
        public Item? item { get; set; }
    }
}
