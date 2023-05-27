using System;
using System.Collections.Generic;

using NUnit.Framework;

public static class StravaSynchronisationApiControllerTestCases
{
    public static IEnumerable<TestCaseData> ImportLatestSaveOrIgnoreTestCases
    {
        get
        {
            yield return new TestCaseData("IceSkate", new DateTime(2023, 6, 1), 1, 0);
            yield return new TestCaseData("InlineSkate", new DateTime(2023, 6, 1), 1, 0);
            yield return new TestCaseData("InlineSkate", new DateTime(2023, 6, 1), 1, 0);
            yield return new TestCaseData("OutdoorWalk", new DateTime(2023, 6, 1), 0, 1);
            yield return new TestCaseData("IceSkate",    new DateTime(2023, 5, 31, 23, 59, 59), 0, 1);
            yield return new TestCaseData("InlineSkate", new DateTime(2023, 5, 31, 23, 59, 59), 0, 1);
            yield return new TestCaseData("InlineSkate", new DateTime(2023, 5, 31, 23, 59, 59), 0, 1);
            yield return new TestCaseData("OutdoorWalk", new DateTime(2023, 5, 31, 23, 59, 59), 0, 1);
        }
    }
}