using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 12f;
    [SerializeField] private float range = 1.8f;
    [SerializeField] private float cooldown = 1.2f;

    private float attackTimer;

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }

    public void TryAttack(GameObject target)
    {
        if (target == null)
            return;

        if (attackTimer < cooldown)
            return;

        if (Vector3.Distance(transform.position, target.transform.position) <= range)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
                attackTimer = 0f;
            }
        }
    }
}
