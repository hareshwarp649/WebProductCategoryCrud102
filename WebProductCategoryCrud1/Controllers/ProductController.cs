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
            
        }

        
       [HttpPost]
       [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(int? id,ProductVM vm )
        {
           
            if (ModelState.IsValid)
            {
                //bool isDuplicate = false;

                //var existingProduct = _unitOfWork.Product.GetT(x => x.ProductName == vm.Product.ProductName && x.CategoryId != id);
                var existingProduct = _unitOfWork.Product.GetT(x => x.ProductName == vm.Product.ProductName && (id == null || x.ProductId != id));
                if (existingProduct != null)
                {

                    ModelState.AddModelError("Name", "A product with the same name already exists Please Update another Product name.");
                    return View(vm);
                }

                
                //string fileName = String.Empty;
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
