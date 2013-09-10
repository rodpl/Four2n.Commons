// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FckEditorHelperTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the FckEditorHelperTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.MonoRail.Helpers
{
    using System.Threading;

    using Castle.MonoRail.Framework;
    using Castle.MonoRail.Framework.Test;

    using NUnit.Framework;

    using Four2n.Commons.MonoRail.Controllers;

    [TestFixture]
    public class FckEditorHelperTests : MockedTestFixture
    {
        private FckEditorHelper sut;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            this.sut = new FckEditorHelper();

            this.sut.SetController(new HomeController(), new ControllerContext());
            this.sut.SetContext(new StubEngineContext(new StubRequest(), new StubResponse(), new UrlInfo("area", "home", "index", "/app", "sdm")));
            this.sut.ServerUtility = new StubServerUtility();

            this.sut.Context.Request.Params["HTTP_USER_AGENT"] = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
            Assert.That(this.sut.IsCompatibleBrowser());
        }

        [Test]
        public void CreateHtmlTest_ReturnsHtmlInDivTags()
        {
            var html = this.sut.CreateHtml("instance");
            Assert.That(html, Is.StringStarting("<div>\n"));
            Assert.That(html, Is.StringEnding("</div>\n"));
        }

        [Test]
        public void InstallScripts_ReturnsLinkToJavaScript()
        {
            Assert.AreEqual(@"<script type=""text/javascript"" src=""/app/content/helpers/fckeditor/fckeditor.js""></script>", this.sut.InstallScripts());
        }

        [Test]
        [SetCulture("en-GB")]
        public void SetCulture_English()
        {
            Assert.That(Thread.CurrentThread.CurrentCulture.Name, Is.EqualTo("en-GB"));
        }

        [Test]
        [SetCulture("pl-PL")]
        public void SetCulture_Polish()
        {
            Assert.That(Thread.CurrentThread.CurrentCulture.Name, Is.EqualTo("pl-PL"));
        }

        // IE 5.5+ on Windows
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322)")]
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; InfoPath.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; Dealio Deskball 3.0)")]
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; NeosBrowser; .NET CLR 1.1.4322; .NET CLR 2.0.50727)")]
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 5.5; Windows 98)")]

        // FireFox 1.5+
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1")]
        [TestCase(@"Mozilla/5.0 (X11; U; Linux x86_64; sv-SE; rv:1.8.1.12) Gecko/20080207 Ubuntu/7.10 (gutsy) Firefox/2.0.0.12")]
        [TestCase(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.8.0.4) Gecko/20060614 Fedora/1.5.0.4-1.2.fc5 Firefox/1.5.0.4 pango-text")]
        [TestCase(@"Mozilla/5.0 (X11; U; Darwin Power Macintosh; en-US; rv:1.8.0.12) Gecko/20070803 Firefox/1.5.0.12 Fink Community Edition")]
        [TestCase(@"Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.8) Gecko/20051201 Firefox/1.5")]

        // Netscape 7.1+
        [TestCase(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en-US; rv:1.8.1.8pre) Gecko/20071001 Firefox/2.0.0.7 Navigator/9.0RC1")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en-US; rv:1.8.1.9pre) Gecko/20071102 Firefox/2.0.0.9 Navigator/9.0.0.3")]
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:1.7.5) Gecko/20050519 Netscape/8.0.1")]
        [TestCase(@"Mozilla/5.0 (Windows; U; WinNT4.0; en-US; rv:1.4) Gecko/20030624 Netscape/7.1 (ax)")]

        // Camino 1.0+
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X Mach-O; en; rv:1.8.1.4pre) Gecko/20070511 Camino/1.6pre")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en; rv:1.8.1.6) Gecko/20070809 Firefox/2.0.0.6 Camino/1.5.1")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en-US; rv:1.8.1) Gecko/20061018 Camino/1.1a1")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en-US; rv:1.8.0.1) Gecko/20060214 Camino/1.0")]

        // Opera 9.5+
        [TestCase(@"Opera/9.51 (Macintosh; Intel Mac OS X; U; en)")]
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; en) Opera 9.51")]
        [TestCase(@"Mozilla/5.0 (X11; Linux i686; U; en; rv:1.8.1) Gecko/20061208 Firefox/2.0.0 Opera 9.51")]

        // Safari 3.0+
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en) AppleWebKit/526.9 (KHTML, like Gecko) Version/4.0dp1 Safari/526.8")]
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-GB) AppleWebKit/525.19 (KHTML, like Gecko) Version/3.1.2 Safari/525.21")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X 10_5_2; en-gb) AppleWebKit/526+ (KHTML, like Gecko) Version/3.1 iPhone")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X; fr) AppleWebKit/523.12.2 (KHTML, like Gecko) Version/3.0.4 Safari/523.12.2")]
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/523.6.1+ (KHTML, like Gecko) Version/3.0 Safari/523.6.1+")]
        [TestCase(@"Mozilla/5.0 (iPhone; U; CPU like Mac OS X; en) AppleWebKit/420+ (KHTML, like Gecko) Version/3.0 Mobile/1C28 Safari/419.3")]
        public void IsCompatibleBrowser_ThisBrowserShouldBeCompatible(string agent)
        {
            this.sut.Context.Request.Params["HTTP_USER_AGENT"] = agent;
            Assert.That(this.sut.IsCompatibleBrowser());
        }

        // IE < 5.5 on Windows
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 5.00; Windows 98)")]
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 4.01; Windows NT 5.0)")]
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 5.01; Windows NT; .NET CLR 1.0.3705)")]

        // IE on Mac
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 5.23; Mac_PowerPC)")]
        [TestCase(@"Mozilla/4.0 (compatible; MSIE 5.5b1; Mac_PowerPC)")]
        [TestCase(@"Mozilla/5.0 (MSIE 7.0; Macintosh; U; SunOS; X11; gu; SV1; InfoPath.2; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648)")]

        // FireFox < 1.5
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8b4) Gecko/20050908 Firefox/1.4")]
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.7.13) Gecko/20060410 Firefox/1.0.8")]
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.2b) Gecko/20020923 Phoenix/0.1")]

        // Netscape < 7.1
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.0.2) Gecko/20030208 Netscape/7.02")]
        [TestCase(@"Mozilla/5.0 (X11; U; SunOS sun4u; en-US; rv:1.0.1) Gecko/20020921 Netscape/7.0")]
        [TestCase(@"Mozilla/5.0 (Windows; U; WinNT4.0; fr-FR; rv:0.9.4.1) Gecko/20020508 Netscape6/6.2.3")]
        [TestCase(@"Mozilla/5.0 (Windows; U; Windows NT 5.0; fr-FR; rv:0.9.2) Gecko/20010726 Netscape6/6.1")]
        [TestCase(@"Mozilla/4.8 [pl] (Windows NT 5.1; U)")]

        // Camino < 1.0 and alpha and beta
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X Mach-O; en-US; rv:1.8b4) Gecko/20050914 Camino/1.0a1")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en-US; rv:1.8b5) Gecko/20051021 Camino/1.0+")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X Mach-O; en-US; rv:1.7) Gecko/20040517 Camino/0.8b")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X Mach-O; en-US; rv:1.0.1) Gecko/20030306 Camino/0.7")]

        // Opera < 9.5
        [TestCase(@"Opera/9.30 (Nintendo Wii; U; ; 2047-7; fr)")]
        [TestCase(@"Mozilla/5.0 (Windows NT 5.2; U; en; rv:1.8.0) Gecko/20060728 Firefox/1.5.0 Opera 9.27")]

        // Safari < 3.0
        [TestCase(@"Mozilla/5.0 (Linux; U; Ubuntu; en-us) AppleWebKit/525.13 (KHTML, like Gecko) Version/2.2 Firefox/525.13")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/418.9 (KHTML, like Gecko) Safari/419.3")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/412.6.2 (KHTML, like Gecko) Safari/412.2.2")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/312.5 (KHTML, like Gecko) Safari/312.3")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X; de-de) AppleWebKit/85.7 (KHTML, like Gecko) Safari/85.5")]
        [TestCase(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en) AppleWebKit/521.32.1 (KHTML, like Gecko) Safari/521.32.1")]

        // Lynx
        [TestCase(@"Lynx/2.8.7dev.4 libwww-FM/2.14 SSL-MM/1.4.1 OpenSSL/0.9.8d")]
        public void IsCompatibleBrowser_ThisBrowserShouldNOTBeCompatible(string agent)
        {
            this.sut.Context.Request.Params["HTTP_USER_AGENT"] = agent;
            Assert.That(!this.sut.IsCompatibleBrowser());
        }
    }
}