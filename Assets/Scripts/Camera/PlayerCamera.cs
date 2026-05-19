using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float smoothTime = 0.25f;
    [Header("World Limits")]
    [SerializeField] Transform limitLeft;
    [SerializeField] Transform limitRight;

    // Internal params
    Transform player;
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

        BlockMovementOutsideLimits();
        SetDesiredPosition();
        MoveX();
    }
    void BlockMovementOutsideLimits()
    {
        bool canMoveLeft = player.position.x >= limitLeft.position.x;
        bool canMoveRight = player.position.x <= limitRight.position.x;

        canFollowX = canMoveLeft && canMoveRight;
    }
    void SetDesiredPosition()
    {
        bool isStillInsideLimits = transform.position.x > limitLeft.position.x && transform.position.x < limitRight.position.x;
        if (canFollowX || isStillInsideLimits)
        {
            desiredPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            cameraVelocity.x = 0f;
        }
    }
    void MoveX()
    {
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, smoothTime);
        
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, limitLeft.position.x, limitRight.position.x);
        
        transform.position = smoothedPosition;
    }
}
