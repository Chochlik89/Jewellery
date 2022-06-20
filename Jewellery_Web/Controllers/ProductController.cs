using Jewellery_DataAccess;
using Jewellery_DataAccess.Repositories.IRepositories;
using Jewellery_Models;
using Jewellery_Models.ViewModels;
using Jewellery_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jewellery.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]

    public class ProductController : Controller
    {
        private readonly IProductRepository _prodRepo;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository prodRepo, IWebHostEnvironment webHostEnvironment)
        {
            _prodRepo = prodRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _prodRepo.GetAll(includeProperties: "Category,MaterialType");

            //foreach (var obj in objList)
            //{
            //    obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            //    obj.MaterialType = _db.MaterialType.FirstOrDefault(u=> u.Id == obj.MaterialTypeId);
            //};

            return View(objList);
        }

        //GET-UPSERT
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            //ViewBag.CategoryDropDown = CategoryDropDown;
            //ViewData["CategoryDropDown"] = CategoryDropDown;

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _prodRepo.GetAllDropdownList(WebConstants.CategoryName),
                MaterialTypeSelectList = _prodRepo.GetAllDropdownList(WebConstants.MaterialTypeName)
            };

            if (id == null)
            {
                //this is for Create
                return View(productVM);
            }
            else
            {
                productVM.Product = _prodRepo.Find(id.GetValueOrDefault());
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        //POST-UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(productVM.Product.Id == 0)
                {
                    //Creating
                    string upload = webRootPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using(var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;

                    _prodRepo.Add(productVM.Product);
                }
                else
                {
                    //Updating
                    var objFromDb = _prodRepo.FirstOrDefault(u => u.Id == productVM.Product.Id, isTracking: false);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WebConstants.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _prodRepo.Update(productVM.Product);
                }
                _prodRepo.Save();
                return RedirectToAction("Index");
            }
            productVM.CategorySelectList = _prodRepo.GetAllDropdownList(WebConstants.CategoryName);
            productVM.MaterialTypeSelectList = _prodRepo.GetAllDropdownList(WebConstants.MaterialTypeName);

            return View(productVM);
        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objToDelete = _prodRepo.FirstOrDefault(u => u.Id == id, includeProperties: "Category,MaterialType");

            if (objToDelete == null)
            {
                return NotFound();
            }
            return View(objToDelete);
        }

        //POST-DELETE
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var objToDelete = _prodRepo.Find(id.GetValueOrDefault());
            if (objToDelete == null)
            {
                return NotFound();

            }

            string upload = _webHostEnvironment.WebRootPath + WebConstants.ImagePath;
            var fileToDelete = Path.Combine(upload, objToDelete.Image);

            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
            }

            _prodRepo.Remove(objToDelete);

            _prodRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
