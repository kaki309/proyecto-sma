using UnityEngine;

public class PlayerCameraVertical : MonoBehaviour
{
    [SerializeField] float smoothTime = 0.25f;
    [Header("World Limits")]
    [SerializeField] Transform limitTop;
    [SerializeField] Transform limitBottom;

    // Internal params
    Transform player;
    bool canFollowY;
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
        MoveY();
    }

    void BlockMovementOutsideLimits()
    {
        bool canMoveDown = player.position.y >= limitBottom.position.y;
        bool canMoveUp = player.position.y <= limitTop.position.y;
        canFollowY = canMoveDown && canMoveUp;
    }

    void SetDesiredPosition()
    {
        bool isStillInsideLimits = transform.position.y < limitTop.position.y && transform.position.y > limitBottom.position.y;
        if (canFollowY || isStillInsideLimits)
        {
            desiredPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
        else
        {
            desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void MoveY()
    {
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, smoothTime);
        transform.position = smoothedPosition;
    }
}