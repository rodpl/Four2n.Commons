// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingTestController.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the BindingTestController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TestWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Domain;

    public class BindingTestController : Controller
    {
        //
        // GET: /BindingTest/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /BindingTest/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BindingTest/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /BindingTest/Create

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
        
        //
        // GET: /BindingTest/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = new TestWeb.Domain.SomeModel();
            model.Id = 8;
            model.Name = "Jonh";
            model.Role = "Anon";
            return View(model);
        }

        //
        // POST: /BindingTest/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, TestWeb.Domain.SomeModel someModel)
        {
            try
            {
                var model = new TestWeb.Domain.SomeModel();
                model.Id = 8;
                model.Name = "Jonh";
                model.Role = "Anon";

                this.UpdateModel(model);

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BindingTest/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BindingTest/Delete/5

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

        public class SomeModelMetadata
        {
            [DisplayName("Rola:")]
            [ReadOnly(true)]
            public string Role { get; set; }
        }
    }
}
