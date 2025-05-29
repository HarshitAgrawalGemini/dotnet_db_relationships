using db_relationship_proj.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using db_relationship_proj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace db_relationship_proj.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _DBcontext;
        public ItemController(ApplicationDbContext context)
        {
            _DBcontext = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Item> item = await _DBcontext.Items.Include(i => i.SerialNumber).Include(i => i.category).ToListAsync();
            

            return View(item);
        }




        [HttpPost]
        public async Task<IActionResult> CreateCat([Bind("Id", "Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _DBcontext.Categories.Add(category);
                await _DBcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult CreateCat()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem([Bind("Id", "Name", "Price", "SerialNumber", "category", "CategoryId")] Item item)
        {

            if (ModelState.IsValid)
            {
                var obj = item;

                foreach (var prop in obj.GetType().GetProperties())
                {
                    var name = prop.Name;
                    var value = prop.GetValue(obj, null);
                    Console.WriteLine($"{name}: {value}");
                }

                //Console.WriteLine(item+""+""+"");
                _DBcontext.Items.Add(item);
                await _DBcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);

        }

        public async Task<IActionResult> CreateItem()
        {
            List<Category> cats = await _DBcontext.Categories.ToListAsync();
            List<Category> cat =  _DBcontext.Categories.ToList();
            ItemAndCategory ViewModel = new ItemAndCategory()
            {
                item = new Item(),
                categories = cats.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return View(ViewModel);
        }

        public async Task<ActionResult> DeleteItem(int id)
        {
            Item item = await _DBcontext.Items.FirstOrDefaultAsync(x => x.Id == id);
            _DBcontext.Items.Remove(item);
            await _DBcontext.SaveChangesAsync();


            return RedirectToAction("Index");
        }
    }
}
