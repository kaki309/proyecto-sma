using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerHealthTests
{
    private PlayerHealth player;

    [SetUp]
    public void SetUp()
    {
        player = new GameObject("Player").AddComponent<PlayerHealth>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(player.gameObject);
    }

    [UnityTest]
    public IEnumerator PlayerStartsWith5Health()
    {
        yield return null;  // Wait one frame for Awake to run
        Assert.AreEqual(5, player.CurrentHealth);
    }

    [UnityTest]
    public IEnumerator PlayerTakesThreePunches_EndsWith2Health()
    {
        yield return null;  // Wait one frame for Awake to run
        
        // Arrange
        int expectedHealth = 2;

        // Act
        player.TakeDamage(1);  // First punch: 1 damage
        player.TakeDamage(1);  // Second punch: 1 damage
        player.TakeDamage();  // Third punch: No damage defined (Default value is 1)

        // Assert
        Assert.AreEqual(expectedHealth, player.CurrentHealth);
    }

    [UnityTest]
    public IEnumerator PlayerIsAliveOnStart()
    {
        yield return null;  // Wait one frame for Awake to run
        Assert.IsTrue(player.IsAlive);
    }

    [UnityTest]
    public IEnumerator PlayerDies_WhenHealthReaches0()
    {
        yield return null;  // Wait one frame for Awake to run
        
        // Act
        player.TakeDamage(player.MaxHealth);

        // Assert
        Assert.AreEqual(0, player.CurrentHealth);
        Assert.IsFalse(player.IsAlive);
    }

    [UnityTest]
    public IEnumerator PlayerDies_WhenDamageExceedsMaxHealth()
    {
        yield return null;  // Wait one frame for Awake to run
        
        // Act
        player.TakeDamage(player.MaxHealth + 50);  // Deal more than max health

        // Assert
        Assert.AreEqual(0, player.CurrentHealth);
        Assert.IsFalse(player.IsAlive);
    }
}

