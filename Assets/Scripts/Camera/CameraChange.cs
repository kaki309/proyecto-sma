using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [Header("Cameras to Toggle")]
    public GameObject cameraA;
    public GameObject cameraB;

    [Header("Settings")]
    public string playerTag = "Player"; // Recomiendo volver a usar "Player" en Unity

    private bool isCameraBActive = false;

    // CORRECCIÓN: Usamos OnTriggerEnter2D y Collider2D para juegos en 2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        // This will tell you if the trigger is detecting ANYTHING
        Debug.Log("🚨 SOMETHING entered the Trigger. Object name: " + other.gameObject.name + " | Tag: " + other.tag);

        if (other.CompareTag(playerTag))
        {
            Debug.Log("✅ It's the player! Switching cameras...");
            isCameraBActive = !isCameraBActive;

            cameraA.SetActive(!isCameraBActive);
            cameraB.SetActive(isCameraBActive);
        }
        else
        {
            Debug.Log("❌ An object entered, but its Tag is NOT " + playerTag);
        }
    }
}