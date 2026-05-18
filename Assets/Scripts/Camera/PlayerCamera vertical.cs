using UnityEngine;

public class PlayerCameraVertical : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] bool enableHorizontalMovement = false; // Antes era vertical
    [Header("World Limits")]
    [SerializeField] Transform limitBottom; // Antes era limitLeft
    [SerializeField] bool ignoreLimits = false;

    // Internal params
    bool canFollowY;
    Vector3 desiredPosition;
    Vector3 cameraVelocity = Vector3.zero;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        if (!ignoreLimits) BlockMovementOutsideLimits();
        SetDesiredPosition();
        MoveY();
        if (enableHorizontalMovement) ClampWidthToPlayer();
    }

    void BlockMovementOutsideLimits()
    {
        // Verifica si el jugador está por encima del límite inferior
        bool canMoveDown = player.position.y >= limitBottom.position.y;
        canFollowY = canMoveDown;
    }

    void SetDesiredPosition()
    {
        if (canFollowY)
        {
            // El objetivo es la Y del jugador, mantenemos la X e Z de la cámara
            desiredPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
        else
        {
            // Se queda quieta en su posición actual
            desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void MoveY()
    {
        // Aplicamos el suavizado en el eje vertical
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, smoothTime);
        transform.position = smoothedPosition;
    }

    void ClampWidthToPlayer()
    {
        // Si quieres que también siga al jugador lateralmente de forma suave
        Vector3 nextPos = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, nextPos, smoothTime);
    }
}