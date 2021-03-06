﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eciWEB2016.Models;
using eciWEB2016.Controllers.DataControllers;
using System.Data;
using System.Web.Services;

namespace eciWEB2016.Controllers
{
    public class TimeSheetController : Controller
    {
        // GET: TimeSheet
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult addTimeSheet(FormCollection col)
        {

            TimeHeaderModel timeHeader = new TimeHeaderModel();

            timeHeader.staffID = Convert.ToInt32(col["staffID"]);
            timeHeader.weekEnding = col["weekEnding"];

            TimeSheetDataController dataController = new TimeSheetDataController();

            dataController.insertTimeHeader(timeHeader);

            Staff staff = new Staff();
            StaffController staffController = new StaffController();
            ViewBag.staffList = staffController.GetStaffList();
            ViewBag.staffID = timeHeader.staffID;

            return View("Time_Headers");
            
        }


        [HttpPost]
        public ActionResult addTimeDetails(FormCollection col)
        {
            TimeDetailModel details = new TimeDetailModel();

            details.timeHeaderID = Convert.ToInt32(col["hiddenID"]);
            details.actualTime = Convert.ToDecimal(col["actualTime"]);
            details.insuranceTime = Convert.ToDecimal(col["insuranceTime"]);
            details.placeOfService = col["placeOfService"];
            details.canceled = col["canceled"];

            int staffID = Convert.ToInt32(col["hiddenStaffID"]);

            TimeSheetDataController dataController = new TimeSheetDataController();

            dataController.insertTimeDetail(details);

            Staff staff = new Staff();
            StaffController staffController = new StaffController();
            ViewBag.staffList = staffController.GetStaffList();
            ViewBag.staffID = staffID;

            return View("Time_Headers");

        }


        public ActionResult TimeSheet_Grid_Partial(int staffID)
        {
            try
            {
                List<TimeHeaderModel> headerList;
                TimeSheetDataController dataController = new TimeSheetDataController();

                headerList = dataController.GetTimeHeaders(staffID);

                return PartialView("TimeSheet_Grid_Partial", headerList);

            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }

        }

        //gets the selected staff member's time sheet
        public ActionResult StaffTimeSheet(int timeHeaderID)
        {
            try
            {
                List<TimeDetailModel> details = new List<TimeDetailModel>();

                TimeSheetDataController dataController = new TimeSheetDataController();

                details = dataController.GetTimeSheet(timeHeaderID);

                return PartialView("TimeDetails_Grid_Partial", details);



            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: TimeSheet/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TimeSheet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeSheet/Create
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

        // GET: TimeSheet/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TimeSheet/Edit/5
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

        // GET: TimeSheet/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TimeSheet/Delete/5
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
