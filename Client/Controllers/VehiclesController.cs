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
    public class VehiclesController : Controller
    {
        private VehicleContext db = new VehicleContext();

        public ActionResult Index()
        {
            var types = db.VehicleType.Select(t => new { TypeID = t.ID, TypeName = t.Brand }).ToList();

            ViewData["VehicleTypes"] = types;
            return View();
        }


        public ActionResult Vehicles_Read([DataSourceRequest]DataSourceRequest request, int ownerID=0)
        {
            IQueryable<Vehicle> vehicles = db.Vehicles;

                var result = vehicles.ToDataSourceResult(request, vehicle => new {
                ID = vehicle.ID,
                RegistrationNumber = vehicle.RegistrationNumber,
                VIN = vehicle.VIN,
                VehicleTypeID = vehicle.VehicleTypeID,
                IsOwned = vehicle.VehicleOwners.Any(vo=>vo.OwnerID == ownerID),
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Vehicles_Create([DataSourceRequest]DataSourceRequest request, Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                var entity = new Vehicle
                {
                    RegistrationNumber = vehicle.RegistrationNumber,
                    VIN = vehicle.VIN,
                    VehicleTypeID = vehicle.VehicleTypeID,
                };

                db.Vehicles.Add(entity);
                db.SaveChanges();
                vehicle.ID = entity.ID;
            }

            return Json(new[] { vehicle }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Vehicles_Update([DataSourceRequest]DataSourceRequest request, Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                var entity = new Vehicle
                {
                    ID = vehicle.ID,
                    RegistrationNumber = vehicle.RegistrationNumber,
                    VIN = vehicle.VIN,
                    VehicleTypeID = vehicle.VehicleTypeID,
                };

                db.Vehicles.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { vehicle }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Vehicles_Destroy([DataSourceRequest]DataSourceRequest request, Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                var entity = new Vehicle
                {
                    ID = vehicle.ID,
                    RegistrationNumber = vehicle.RegistrationNumber,
                    VIN = vehicle.VIN,
                    VehicleTypeID = vehicle.VehicleTypeID,
                };

                db.Vehicles.Attach(entity);
                db.Vehicles.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { vehicle }.ToDataSourceResult(request, ModelState));
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

    public class Vehicle2
    {
        public int ID { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public int VehicleTypeID { get; set; }
        public bool IsOwned { get; set; }
    }

}
