using UnityEngine;

public class SoulBlock : MonoBehaviour
{
    [SerializeField] private string rarity = "Rare";
    [SerializeField] private float pickupRadius = 0.75f;

    private bool hasBeenCollected;

    private void Update()
    {
        if (hasBeenCollected)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out VRControllerGrabber grabber))
            {
                hasBeenCollected = true;
                Destroy(gameObject);
                break;
            }
        }
    }

    public string Rarity => rarity;
}
