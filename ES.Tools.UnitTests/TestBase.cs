using System;
using System.IO.Packaging;
using NUnit.Framework;

namespace ES.Tools.UnitTests
{
  public class TestBase
  {
    protected virtual void PackURIFix()
    {
      //System.IO.Packaging.PackUriHelper.Create(new Uri("reliable://0"));
      //var fe = new FrameworkElement();
      const string scheme = "pack";

      if (!UriParser.IsKnownScheme(scheme))
      {
        Assert.That(PackUriHelper.UriSchemePack, Is.EqualTo(scheme));
      }
    }
  }
}
