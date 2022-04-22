using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.PerformanceTesting;
using Cr7Sund.EditorUtils;

public class TestPerferenceTools
{

    [Test, Performance]
    public void TestCreateElement()
    {
        int count = 0;
        Measure.Method(() => Assert.AreEqual(PeferenceSettingWindow.DrawElements().childCount, count))
        .WarmupCount(10)
        .MeasurementCount(10)
        .IterationsPerMeasurement(5)
        .SetUp(() => count = PeferenceSettingWindow.TestFullReflection())
        .GC()
        .Run();
    }
    [Test, Performance]
    public void TestFullReflection()
    {
        Measure.Method(() => PeferenceSettingWindow.TestFullReflection())
        .WarmupCount(10)
        .MeasurementCount(10)
        .IterationsPerMeasurement(5)
        .GC()
        .Run();
    }

    [Test, Performance]
    public void TestReflection_Optimized()
    {
        int count = 0;
        Measure.Method(() => Assert.AreEqual(PeferenceSettingWindow.TestReflection_Optimized(), count))
        .WarmupCount(10)
        .MeasurementCount(10)
        .IterationsPerMeasurement(5)
        .SetUp(() => count = PeferenceSettingWindow.TestFullReflection())
        .GC()
        .Run();
    }

    [Test, Performance]
    public void TestReflection_Optimized_2()
    {
        int count = 0;
        Measure.Method(() =>
        {
            int a = PeferenceSettingWindow.TestReflection_Optimized2();
            Assert.AreEqual(a, count);
        })
        .WarmupCount(10)
        .MeasurementCount(10)
        .IterationsPerMeasurement(5)
        .SetUp(() => count = PeferenceSettingWindow.TestFullReflection())
        .GC()
        .Run();
    }



    [Test, Performance]
    public void TestReflection_WithouAttribute()
    {
        Measure.Method(() => PeferenceSettingWindow.TestReflection_WithouAttribute())
        .WarmupCount(10)
        .MeasurementCount(10)
        .IterationsPerMeasurement(5)
        .GC()
        .Run();
    }
    [Test, Performance]
    public void TestReflection_WithouProperteis()
    {
        Measure.Method(() => PeferenceSettingWindow.TestReflection_WithouProperteis())
        .WarmupCount(10)
        .MeasurementCount(10)
        .IterationsPerMeasurement(5)
        .GC()
        .Run();
    }
}
