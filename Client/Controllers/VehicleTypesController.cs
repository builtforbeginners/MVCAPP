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
    public class VehicleTypesController : Controller
    {
        private VehicleContext db = new VehicleContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VehicleType_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<VehicleType> vehicletype = db.VehicleType;
            DataSourceResult result = vehicletype.ToDataSourceResult(request, vehicleType => new {
                ID = vehicleType.ID,
                Brand = vehicleType.Brand,
                Mark = vehicleType.Mark
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VehicleType_Create([DataSourceRequest]DataSourceRequest request, VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                var entity = new VehicleType
                {
                    Brand = vehicleType.Brand,
                    Mark = vehicleType.Mark
                };

                db.VehicleType.Add(entity);
                db.SaveChanges();
                vehicleType.ID = entity.ID;
            }

            return Json(new[] { vehicleType }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VehicleType_Update([DataSourceRequest]DataSourceRequest request, VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                var entity = new VehicleType
                {
                    ID = vehicleType.ID,
                    Brand = vehicleType.Brand,
                    Mark = vehicleType.Mark
                };

                db.VehicleType.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { vehicleType }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VehicleType_Destroy([DataSourceRequest]DataSourceRequest request, VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                var entity = new VehicleType
                {
                    ID = vehicleType.ID,
                    Brand = vehicleType.Brand,
                    Mark = vehicleType.Mark
                };

                db.VehicleType.Attach(entity);
                db.VehicleType.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { vehicleType }.ToDataSourceResult(request, ModelState));
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
