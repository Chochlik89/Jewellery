using Jewellery_DataAccess;
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
        private readonly ApplicationDbContext _db;

        public MaterialTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<MaterialType> objList = _db.MaterialType;
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
            _db.MaterialType.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-EDIT
        public IActionResult Edit(int? id) // I forgot "?" during course assignment.
        {
            if (id == null || id == 0) // I forgot about this condition during course assignment,
                                       // but it is redundant because Find() method returns null for these values.
            {
                return NotFound();
            }

            var obj = _db.MaterialType.Find(id);
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
            if (ModelState.IsValid) // I forgot about this condition during course assignment.
            {
                //_db.MaterialType.Find(obj.Id).Name = obj.Name;
                _db.MaterialType.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            var obj = _db.MaterialType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        // My solution does not secure from having null object.

        /*public IActionResult Delete(MaterialType obj)
        {
            _db.MaterialType.Remove(obj);

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        */

        public IActionResult DeletePost(int? id)
        {
            var obj = _db.MaterialType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.MaterialType.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
