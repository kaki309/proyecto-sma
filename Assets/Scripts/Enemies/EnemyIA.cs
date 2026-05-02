using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float speed = 2f;
    [SerializeField] Transform patrolPointA;
    [SerializeField] Transform patrolPointB;

    GameObject player;
    bool chase = false;
    bool isPatrolling = true;
    Vector3 startingPos;
    Vector2 directionToPlayer;
    Vector3 currentPatrolTarget;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPos = transform.position;
        currentPatrolTarget = patrolPointA.position;
    }

    void Update()
    {
        if (player == null) return;

        CheckPlayerDirection();
        SetSpriteDirection();

        if (chase)
        {
            ChasePlayer();
            isPatrolling = false;
        }
        else
        {
            if (isPatrolling)
            {
                Patrol();
            }
            else
            {
                ReturnToOrigin();
            }
        }
    }

    void CheckPlayerDirection()
    {
        directionToPlayer = (player.transform.position - transform.position).normalized;
    }
    void Patrol()
    {
        // Move towards current patrol target
        transform.position = Vector2.MoveTowards(transform.position, currentPatrolTarget, speed * Time.deltaTime);

        // Switch target when reaching current patrol point
        if (Vector3.Distance(transform.position, currentPatrolTarget) < 0.1f)
        {
            currentPatrolTarget = (currentPatrolTarget == patrolPointA.position) ? patrolPointB.position : patrolPointA.position;
        }
    }
    void SetSpriteDirection()
    {
        int spriteDirection = directionToPlayer.x < 0 ? -1 : 1;
        Vector3 currentScale = transform.localScale;
        currentScale.x = spriteDirection;
        transform.localScale = currentScale;
    }
    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    void ReturnToOrigin()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPos, speed * Time.deltaTime);

        // When reached starting position, start patrolling
        if (Vector3.Distance(transform.position, startingPos) < 0.1f)
        {
            isPatrolling = true;
        }
    }
    void CheckLineOfSightToPlayer()
    {
        // Cast a single raycast towards the player, ignoring the enemy's own layer
        Vector2 rayDirection = (player.transform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, distance);

        // Only chase if the raycast directly hits the player
        if (hit.collider != null)
        {
            chase = hit.collider.gameObject.CompareTag("Player");
        }
        else
        {
            chase = false;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckLineOfSightToPlayer();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            chase = false;
        }
    }
    // Make damage to the player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
    }
}
