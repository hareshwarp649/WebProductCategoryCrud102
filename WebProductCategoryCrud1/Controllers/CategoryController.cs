using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebProductCategoryCrud1.Data;
using WebProductCategoryCrud1.Infrastrucure.IRepository;
using WebProductCategoryCrud1.Infrastrucure.IService;
using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _context;

        private IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pg=1)
        {
            // var categories = _unitOfWork.Category.GetAll();
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();

            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = categories.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = categories.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category, int id)
        {
            
            if(_unitOfWork.Category==category)
            {
                _unitOfWork.Category.Delete(category);
                
            }
            
            category.CategoryId = 0;
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _unitOfWork.Category.GetT(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Update Done!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Category.GetT(x=>x.CategoryId==id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _unitOfWork.Category.GetT(x => x.CategoryId == id);
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }



        
    }
}
