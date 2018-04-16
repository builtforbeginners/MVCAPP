﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DBModel.Model;
using DBModel;

namespace Client.Controllers
{
    public class OwnersController : Controller
    {
        private VehicleContext db = new VehicleContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Owners_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Owner> owners = db.Owners;
            DataSourceResult result = owners.ToDataSourceResult(request, owner => new {
                ID = owner.ID,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Address = owner.Address
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Owners_Create([DataSourceRequest]DataSourceRequest request, Owner owner)
        {
            if (ModelState.IsValid)
            {
                var entity = new Owner
                {
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Address = owner.Address
                };

                db.Owners.Add(entity);
                db.SaveChanges();
                owner.ID = entity.ID;
            }

            return Json(new[] { owner }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Owners_Update([DataSourceRequest]DataSourceRequest request, Owner owner)
        {
            if (ModelState.IsValid)
            {
                var entity = new Owner
                {
                    ID = owner.ID,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Address = owner.Address
                };

                db.Owners.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { owner }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Owners_Destroy([DataSourceRequest]DataSourceRequest request, Owner owner)
        {
            if (ModelState.IsValid)
            {
                var entity = new Owner
                {
                    ID = owner.ID,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Address = owner.Address
                };

                db.Owners.Attach(entity);
                db.Owners.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { owner }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
