// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplatesController.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the TemplatesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TestWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Rod.Commons.System;

    public class TemplatesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DateRangeNullableTests()
        {
            var model = new DateRangeViewModel();
            model.Name = "Nazwa";
            model.DateRange = new DateRange(null, DateTime.Today);
            return View("DateRangeTests");
        }

        public ActionResult DateRangeTests()
        {
            var model = new DateRangeViewModel();
            model.Name = "Nazwa";
            model.DateRange = new DateRange(null, DateTime.Today);
            model.UsualDate = DateTime.Today;
            model.DateRangeWithFormat = new DateRange(null, DateTime.Today);
            return View(model);
        }

        [HttpPost]
        public ActionResult DateRangeTests(DateRangeViewModel model)
        {
            return View(model);
        }

        public ActionResult DateTimeRangeNullableTests()
        {
            var model = new DateTimeRangeViewModel();
            model.Name = "Nazwa";
            model.DateTimeRange = new DateTimeRange(null, DateTime.Today);
            return View("DateTimeRangeTests");
        }

        public ActionResult DateTimeRangeTests()
        {
            var model = new DateTimeRangeViewModel();
            model.Name = "Nazwa";
            model.DateTimeRange = new DateTimeRange(null, DateTime.Today);
            return View(model);
        }

        [HttpPost]
        public ActionResult DateTimeRangeTests(DateTimeRangeViewModel model)
        {
            return View(model);
        }

        public class DateRangeViewModel
        {
            [Required]
            public string Name { get; set; }

            [Display(Order = 5)]
            [DisplayName("Valid period date with time:")]
            public DateRange DateRange { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime UsualDate { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateRange DateRangeWithFormat { get; set; }
        }

        public class DateTimeRangeViewModel
        {
            [Required]
            public string Name { get; set; }

            [Display(Order = 5)]
            [DisplayName("Valid period date with time:")]
            public DateTimeRange DateTimeRange { get; set; }
        }
    }
}
