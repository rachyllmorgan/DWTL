using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DWTL.Controllers
{
    public class DWTLController : Controller
    {
        // GET: DWTL
        public ActionResult Index()
        {
            return View();
        }

        // GET: DWTL/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DWTL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DWTL/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DWTL/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DWTL/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DWTL/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DWTL/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
