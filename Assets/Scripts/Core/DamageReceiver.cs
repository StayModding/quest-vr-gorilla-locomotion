using UnityEngine;

public class DamageReceiver : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private bool destroyOnDeath = false;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
    }

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
}

public interface IDamageable
{
    void TakeDamage(float damage);
}
