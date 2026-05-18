using System;
using UnityEngine;
using UnityEngine.Events;

public class CameraChange : MonoBehaviour
{
    [SerializeField] Camera cameraToSet;
    [SerializeField] Camera[] camerasToDisable;
    public static Action<Camera> onCameraChanged;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraToSet.enabled = true;
            onCameraChanged?.Invoke(cameraToSet);
            foreach (Camera cam in camerasToDisable)
            { cam.enabled = false; }
        }
    }
}