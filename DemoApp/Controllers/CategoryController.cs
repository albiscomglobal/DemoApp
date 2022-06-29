using DemoApp.Data;
using DemoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET action method
        public IActionResult Create()
        {

            return View();
        }

        //POST action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {

                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");


            }
            return View(obj);
        }




        //GET action method for the edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }
            var categoryfromDb = _db.Categories.Find(id);
            //var categoryfromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryfromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);


            if (categoryfromDb == null)
            {

                return NotFound();
            }
            return View(categoryfromDb);
        }

        //POST action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {

                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");


            }
            return View(obj);
        }




        //Delete action method for the Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }
            var categoryfromDb = _db.Categories.Find(id);
            //var categoryfromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryfromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);


            if (categoryfromDb == null)
            {

                return NotFound();
            }
            return View(categoryfromDb);
        }

        //POST action method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {

                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");


        }
    }
}
