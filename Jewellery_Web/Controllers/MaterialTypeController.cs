using Jewellery_DataAccess;
using Jewellery_DataAccess.Repositories.IRepositories;
using Jewellery_Models;
using Jewellery_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Jewellery.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]

    public class MaterialTypeController : Controller
    {
        private readonly IMaterialTypeRepository _matTypeRepo;

        public MaterialTypeController(IMaterialTypeRepository matTypeRepo)
        {
            _matTypeRepo = matTypeRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<MaterialType> objList = _matTypeRepo.GetAll();
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MaterialType obj)
        {
            _matTypeRepo.Add(obj);
            _matTypeRepo.Save();
            return RedirectToAction("Index");
        }

        //GET-EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _matTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MaterialType obj)
        {
            if (ModelState.IsValid)
            {
                _matTypeRepo.Update(obj);
                _matTypeRepo.Save();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            var obj = _matTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _matTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            _matTypeRepo.Remove(obj);
            _matTypeRepo.Save();
            return RedirectToAction("Index");
        }
    }

}
