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
    public class VehicleOwnerController : Controller
    {
        private VehicleContext db = new VehicleContext();

        public ActionResult Owner(int ownerID)
        {
            var types = db.VehicleType.Select(t => new { TypeID = t.ID, TypeName = t.Brand }).ToList();
            ViewData["VehicleTypes"] = types;

            var model = db.Owners.FirstOrDefault(o => o.ID == ownerID);
            return View(model);
        }

        public ActionResult Vehicle(int vehicleID)
        {
            var model = db.Vehicles.FirstOrDefault(o => o.ID == vehicleID);
            return View(model);
        }

        public void AddVehicle(int vehicleID, int ownerID)
        {
            db.VehicleOwners.Add(new VehicleOwner
            {
                VehicleID = vehicleID,
                OwnerID = ownerID,
            });
            db.SaveChanges();
        }

        public void RemoveVehicle(int vehicleID, int ownerID)
        {
            var v = db.VehicleOwners.FirstOrDefault(vo => vo.OwnerID == ownerID && vo.VehicleID == vehicleID);
            if (v != null)
            {
                db.VehicleOwners.Remove(v);
                db.SaveChanges();
            }
        }


        public ActionResult VehicleOwners_Read([DataSourceRequest]DataSourceRequest request, int vehicleID = 0, int ownerID = 0)
        {
            IQueryable<VehicleOwner> vehicleowners = db.VehicleOwners;

            if (vehicleID != 0)
            {
                vehicleowners = vehicleowners.Where(vo => vo.VehicleID == vehicleID).Include(c => c.Vehicle).Include(c => c.Vehicle.VehicleType);
            }

            if (ownerID != 0)
            {
                vehicleowners = vehicleowners.Where(vo => vo.OwnerID == ownerID).Include(c => c.Owner).Include(c => c.Vehicle.VehicleType); ;
            }


            DataSourceResult result = vehicleowners.ToDataSourceResult(request, vehicleOwner => new {
                ID = vehicleOwner.ID,
                VehicleID = vehicleOwner.VehicleID,
                OwnerID = vehicleOwner.OwnerID,
                FirstName = vehicleOwner.Owner.FirstName,
                LastName = vehicleOwner.Owner.LastName,
                VehicleModel = vehicleOwner.Vehicle?.VehicleType?.Brand,
                VehicleNumber = vehicleOwner.Vehicle.RegistrationNumber,
                VehicleVIN = vehicleOwner.Vehicle.VIN
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VehicleOwners_CreateByOwner([DataSourceRequest]DataSourceRequest request, VehicleOwner vehicleOwner, int ownerID=0)
        {
            if (ModelState.IsValid)
            {
                var entity = new VehicleOwner
                {
                    VehicleID = vehicleOwner.VehicleID,
                    OwnerID = ownerID,
                };

                db.VehicleOwners.Add(entity);
                db.SaveChanges();
                vehicleOwner.ID = entity.ID;
            }

            return Json(new[] { vehicleOwner }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VehicleOwners_CreateByVehicle([DataSourceRequest]DataSourceRequest request, VehicleOwner vehicleOwner, int vehicleID=0)
        {
            if (ModelState.IsValid)
            {
                var entity = new VehicleOwner
                {
                    VehicleID = vehicleID,
                    OwnerID = vehicleOwner.OwnerID,
                };

                db.VehicleOwners.Add(entity);
                db.SaveChanges();
                vehicleOwner.ID = entity.ID;
            }

            return Json(new[] { vehicleOwner }.ToDataSourceResult(request, ModelState));
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VehicleOwners_Update([DataSourceRequest]DataSourceRequest request, VehicleOwner vehicleOwner)
        {
            if (ModelState.IsValid)
            {
                var entity = new VehicleOwner
                {
                    ID = vehicleOwner.ID,
                    VehicleID = vehicleOwner.VehicleID,
                    OwnerID = vehicleOwner.OwnerID,
                };

                db.VehicleOwners.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { vehicleOwner }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VehicleOwners_Destroy([DataSourceRequest]DataSourceRequest request, VehicleOwner vehicleOwner)
        {
            if (ModelState.IsValid)
            {
                var entity = new VehicleOwner
                {
                    ID = vehicleOwner.ID,
                    VehicleID = vehicleOwner.VehicleID,
                    OwnerID = vehicleOwner.OwnerID,
                };

                db.VehicleOwners.Attach(entity);
                db.VehicleOwners.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { vehicleOwner }.ToDataSourceResult(request, ModelState));
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
