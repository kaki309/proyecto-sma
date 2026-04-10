using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerHealthTests
{
    private GameObject playerObject;
    private Player player;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject("Player");
        player = playerObject.AddComponent<Player>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(playerObject);
    }

    [UnityTest]
    public IEnumerator PlayerStartsWith100Health()
    {
        yield return null;  // Wait one frame for Awake to run
        Assert.AreEqual(100f, player.CurrentHealth);
    }

    [UnityTest]
    public IEnumerator PlayerTakesThreePunches_EndsWith30Health()
    {
        yield return null;  // Wait one frame for Awake to run
        
        // Arrange
        float expectedHealth = 30f;

        // Act
        player.TakeDamage(20);  // First punch: 20 damage (100 - 20 = 80)
        player.TakeDamage(25);  // Second punch: 25 damage (80 - 25 = 55)
        player.TakeDamage(25);  // Third punch: 25 damage (55 - 25 = 30)

        // Assert
        Assert.AreEqual(expectedHealth, player.CurrentHealth);
    }

    [UnityTest]
    public IEnumerator PlayerIsAlive()
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
        Assert.AreEqual(0f, player.CurrentHealth);
        Assert.IsFalse(player.IsAlive);
    }

    [UnityTest]
    public IEnumerator PlayerDies_WhenDamageExceedsMaxHealth()
    {
        yield return null;  // Wait one frame for Awake to run
        
        // Act
        player.TakeDamage(player.MaxHealth+50);  // Deal more than max health

        // Assert
        Assert.AreEqual(0f, player.CurrentHealth);
        Assert.IsFalse(player.IsAlive);
    }
}
