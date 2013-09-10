// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelperTestFixture.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the HelperTestFixture type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.MonoRail.Helpers
{
    using Castle.MonoRail.Framework;
    using Castle.MonoRail.Framework.Helpers;
    using Castle.MonoRail.Framework.Test;

    using Four2n.Commons.MonoRail.Controllers;

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