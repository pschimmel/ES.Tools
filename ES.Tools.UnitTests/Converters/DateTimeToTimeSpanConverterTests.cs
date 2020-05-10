using System;
using System.Collections.Generic;
using System.Globalization;
using ES.Tools.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Converters
{
  /// <summary>
  /// Unit tests testing the <see cref="DateTimeToTimeSpanConverter"/>
  /// </summary>
  public class DateTimeToTimeSpanConverterTests
  {
    private static readonly List<TimeSpan?> validValues = new List<TimeSpan?>
    {
      { null },
      { new TimeSpan(9, 14, 21) },
    };

    [Test]
    public void DateTimeToTimeSpanConverterTestValidValues()
    {
      var converter = new DateTimeToTimeSpanConverter();

      foreach (var time in validValues)
      {
        var dateResult = (DateTime?)converter.Convert(time, typeof(DateTime?), null, CultureInfo.InvariantCulture);
        var expectedDate = time == null ? (DateTime?)null : new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, time.Value.Hours, time.Value.Minutes, time.Value.Seconds);
        Assert.AreEqual(expectedDate, dateResult);

        var timeResult = (TimeSpan?)converter.ConvertBack(dateResult, typeof(TimeSpan?), null, CultureInfo.InvariantCulture);
        Assert.AreEqual(time, timeResult);
      }
    }
  }
}