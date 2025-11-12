using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTests
{
    [Test]
    public void EnemyDataMapFunction()
    {
        // Arrange our data
        float testVariable = 1;

        float minOriginalRange = 0;
        float maxOriginalRange = 10;

        float minNewRange = 0;
        float maxNewRange = 1;

        // Act

        float result = EnemyData.Map(testVariable,
                                     minOriginalRange, maxOriginalRange, 
                                     minNewRange, maxNewRange);

        // Assert

        Assert.AreEqual(0.1f, result);
    }
}
