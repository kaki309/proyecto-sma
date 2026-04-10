using UnityEngine;

public class Player : Being
{
    protected override void Die()
    {
        Debug.Log("Player died!");
        // TODO: Handle death (game over screen, respawn, etc)
    }
}
