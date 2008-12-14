//------------------------------------------------------------------------------------------------- 
// <copyright file="HelperTestFixture.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the HelperTestFixture type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.MonoRail.Helpers
{
    using Castle.MonoRail.Framework;
    using Castle.MonoRail.Framework.Helpers;
    using Castle.MonoRail.Framework.Test;

    using Rod.Commons.MonoRail.Controllers;

    public abstract class HelperTestFixture<T>
        where T : AbstractHelper
    {
        protected void InitializeSut(T sut)
        {
            sut.SetController(new HomeController(), new ControllerContext());
            sut.SetContext(new StubEngineContext(new StubRequest(), new StubResponse(), new UrlInfo("area", "home", "index", "/app", "sdm")));
            sut.Context.Request.Params["HTTP_USER_AGENT"] = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
        }
    }
}