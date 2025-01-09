using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProductCategoryCrud1.Data;
using WebProductCategoryCrud1.Infrastrucure.IRepository;
using WebProductCategoryCrud1.Models;
using WebProductCategoryCrud1.ViewModel;

namespace WebProductCategoryCrud1.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region APICALL
        public IActionResult AllProducts()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return Json(new {data= products });
        }
        #endregion

        public IActionResult Index()
        {
           // ProductVM productVM=new ProductVM();
           // productVM.Products = _unitOfWork.Product.GetAll();

            //const int pageSize = 5;
            //if (pg < 1)
            //    pg = 1;
            //int recsCount = products.Count();
            //var pager = new Pager(recsCount, pg, pageSize);
            //int recSkip = (pg - 1) * pageSize;
            //var data = products.Skip(recSkip).Take(pageSize).ToList();
            //this.ViewBag.Pager = pager;
            //return View(data);
            return View();
        }

        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product=new(),
                Categories=_unitOfWork.Category.GetAll().Select(x=>
                new SelectListItem()
                {
                    Text=x.CategoryName,
                    Value=x.CategoryId.ToString()

                })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unitOfWork.Product.GetT(x => x.ProductId == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }else
                {
                    return View(vm);
                }
            }
            //ViewBag.CategoryId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories, "CategoryId", "CategoryName");
            //return View();
        }

        
       [HttpPost]
       [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm)
        {
           
            if (ModelState.IsValid)
            {

                //if (_unitOfWork.Product.Any(p => p.ProductName == vm.ProductName && p.CategoryId == vm.CategoryId))
                //{
                //    ModelState.AddModelError(string.Empty, "Duplicate product entry not allowed.");
                //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", vm.Products);
                //    return View(vm);
                //}

                string fileName = String.Empty;
                if (vm.Product.ProductId == 0)
                {
                    _unitOfWork.Product.Add(vm.Product);
                    TempData["success"] = "Product Created Done!";
                }
                else
                {
                    _unitOfWork.Product.Update(vm.Product);
                    TempData["success"] = "Product Update Done !";
                }

                // ViewBag.CategoryId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #region DeleteAPICALL
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Product.GetT(x => x.ProductId == id);
            if (product == null)
            {
                return Json(new { Success = false, Error = "Error in Fenching Data" });
            }
            else
            {
                _unitOfWork.Product.Delete(product);
                _unitOfWork.Save();
                return Json(new { Success = true, message = "Product Delete" });
            }
        }
        #endregion





    }
}
