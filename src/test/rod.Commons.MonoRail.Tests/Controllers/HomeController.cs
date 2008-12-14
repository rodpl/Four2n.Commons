//------------------------------------------------------------------------------------------------- 
// <copyright file="HomeController.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the HomeController type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.MonoRail.Controllers
{
    using Castle.MonoRail.Framework;

    internal class HomeController : Controller
    {
        public void Index()
        {
        }

        public void Other()
        {
            this.RenderView("display");
        }
    }
}