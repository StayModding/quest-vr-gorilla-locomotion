using UnityEngine;

public class WebShooterAbility : AbilityBase
{
    [SerializeField] private float force = 18f;
    [SerializeField] private float range = 25f;

    public override void Activate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Rigidbody rb = hit.rigidbody;
            if (rb != null)
            {
                rb.AddForce((hit.point - transform.position).normalized * force, ForceMode.VelocityChange);
            }
        }
    }

    public override void Deactivate()
    {
        // no-op
    }
}
