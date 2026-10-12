using UnityEngine;

public class GunAbility : AbilityBase
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float fireRate = 0.15f;
    [SerializeField] private float range = 35f;

    private float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;
    }

    public override void Activate()
    {
        if (fireTimer < fireRate)
            return;

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (hit.collider.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(damage);
            }
        }

        fireTimer = 0f;
    }

    public override void Deactivate()
    {
        // no-op
    }
}
