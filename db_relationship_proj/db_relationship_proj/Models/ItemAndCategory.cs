//using db_relationship_proj.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace db_relationship_proj.Models
{
    public class ItemAndCategory
    {
        public Item item {  get; set; }
        public List<SelectListItem> categories { get; set; }
    }
}
