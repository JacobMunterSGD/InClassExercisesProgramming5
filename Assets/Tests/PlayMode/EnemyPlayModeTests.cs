using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyPlayModeTests
{
    [Test]
    public IEnumerator TakeOneDamage()
    {
        // Arrange
        GameObject gameObject = new();
        Enemy enemy = gameObject.AddComponent<Enemy>();

        yield return null;

        // Act
        enemy.UpdateHealth(1);
        

        // Assert
        Assert.AreEqual(9, enemy.Health);

        Object.Destroy(gameObject);
    }

    [Test]
    public IEnumerator TakeAllTheDamage()
    {
        // Arrange
        GameObject gameObject = new();
        Enemy enemy = gameObject.AddComponent<Enemy>();

        yield return null;

        // Act
        enemy.UpdateHealth(enemy.Health);


        // Assert
        Assert.AreEqual(0, enemy.Health);
        Assert.AreEqual(true, enemy.IsDead);

        Object.Destroy(gameObject);
    }

}
