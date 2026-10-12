using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.2f;
    [SerializeField] private float attackRange = 1.4f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1.1f;
    [SerializeField] private float rotationSpeed = 5f;

    private GameObject currentTarget;
    private float timeSinceLastAttack;

    private void Start()
    {
        GameManager.Instance?.RegisterEnemy(gameObject);
    }

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        currentTarget = GameManager.Instance.GetClosestPlayer(transform.position);
        if (currentTarget == null)
            return;

        Vector3 toTarget = currentTarget.transform.position - transform.position;
        float distance = toTarget.magnitude;

        if (distance > 0.01f)
        {
            Vector3 direction = toTarget.normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= attackCooldown)
            {
                TryAttack();
                timeSinceLastAttack = 0f;
            }
        }
    }

    private void TryAttack()
    {
        if (currentTarget == null)
            return;

        var damageReceiver = currentTarget.GetComponent<IDamageable>();
        if (damageReceiver != null)
        {
            damageReceiver.TakeDamage(attackDamage);
        }
    }
}
