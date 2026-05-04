using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int attackDamage = 1;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}