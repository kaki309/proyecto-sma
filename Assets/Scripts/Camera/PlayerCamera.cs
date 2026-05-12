using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] bool enableVerticalMovement = false;
    [Header("World Limits")]
    [SerializeField] Transform limitLeft;
    [SerializeField] bool ignoreLimits = false;

    // Internal params
    bool canFollowX;
    Vector3 desiredPosition;
    Vector3 cameraVelocity = Vector3.zero; // Used by SmoothDamp

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
        MoveX();
        if (enableVerticalMovement) ClampHeightToPlayer();
    }
    void BlockMovementOutsideLimits()
    {
        bool canMoveLeft = player.transform.position.x >= limitLeft.position.x;

        // canFollowX gets asigned based on the previous comprobation
        canFollowX = canMoveLeft;
    }
    void SetDesiredPosition()
    {
        if (canFollowX)
        {
            // Go to player's position
            desiredPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            // Stay still in its own position
            desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
    void MoveX()
    {
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, smoothTime);
        transform.position = smoothedPosition;
    }
    void ClampHeightToPlayer()
    {
        Vector3 nextPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, nextPos, smoothTime);
    }
}
