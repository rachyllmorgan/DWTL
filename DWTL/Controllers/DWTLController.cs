using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DWTL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;

namespace DWTL.Controllers
{
    public class DWTLController : Controller
    {
        private DownContext context = new DownContext();

        public DownRepository Reposit { get; set; }

        public DWTLController() : base()
        {
            Reposit = new DownRepository();
        }

        // GET: DWTL
        public ActionResult Index()
        {
            List<Competition> random_comp = Reposit.GetRandomCompetitions().ToList();
            return View(random_comp);
        }

        public ActionResult Competitions()
        {
            ViewBag.Message = "All Current Competitions";

            //List<Competition> all_comps = Repo.GetAllCompetitions();
            // How you send a list of anything to a view
            return View(Reposit.GetAllCompetitions());
        }

        public ActionResult UserProfile()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser real_user = Reposit.Context.Users.FirstOrDefault(u => u.Id == user_id);
            DownUser me = Reposit.GetAllUsers().Where(u => real_user.UserName == u.Handle).Single();
            DownUser current_user = Reposit.GetUserByHandle(me.Handle);
            return View(me);
        }

        public ActionResult CreateGroup()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific 
        //properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Name,Bet")] Competition comp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Repo.CreateCompetition(comp);
        //        Repo.Context.SaveChanges();
        //        return RedirectToAction("DWTL");
        //    }

        //    return View(comp);
        //}

        // GET: DWTL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition comp = context.Competitions.Find(id);
            if (comp == null)
            {
                return HttpNotFound();
            }
            return View(comp);
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
