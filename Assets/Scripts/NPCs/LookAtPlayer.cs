using UnityEngine;

public class NPCLookAtPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LookAtPlayer();
        }
    }
    void LookAtPlayer()
    {
        Vector3 scale = transform.localScale;

        scale.x = player.position.x < transform.position.x
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}