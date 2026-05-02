using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Transform patrolPointA;
    [SerializeField] Transform patrolPointB;

    GameObject player;
    SpriteRenderer sprite;
    NavMeshAgent agent;
    bool chase = false;
    bool isPatrolling = true;
    Vector3 startingPos;
    Vector2 currentMovingDirection;
    Vector3 currentPatrolTarget;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPos = transform.position;
        currentPatrolTarget = patrolPointA.position;
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (player == null) return;

        UpdateMovingDirection();
        SetSpriteDirection();
        HandleMovement();
    }

    void UpdateMovingDirection()
    {
        // Calculate direction to current target based on state
        Vector3 targetPosition;
        if (chase)
        {
            targetPosition = player.transform.position;
        }
        else if (isPatrolling)
        {
            targetPosition = currentPatrolTarget;
        }
        else
        {
            targetPosition = startingPos;
        }

        currentMovingDirection = (targetPosition - transform.position).normalized;
    }
    void SetSpriteDirection()
    {
        bool facingLeft = currentMovingDirection.x < 0;
        sprite.flipX = facingLeft;
    }
    void HandleMovement()
    {
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
    void Patrol()
    {
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(currentPatrolTarget);

            // Switch target when reaching current patrol point
            if (Vector3.Distance(transform.position, currentPatrolTarget) < 0.5f)
            {
                currentPatrolTarget = (currentPatrolTarget == patrolPointA.position) ? patrolPointB.position : patrolPointA.position;
            }
        }
    }
    void ChasePlayer()
    {
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(player.transform.position);
        }
    }
    void ReturnToOrigin()
    {
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(startingPos);

            // When reached starting position, start patrolling
            if (Vector3.Distance(transform.position, startingPos) < 0.5f)
            {
                isPatrolling = true;
            }
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
