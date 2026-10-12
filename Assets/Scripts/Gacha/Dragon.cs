using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField] private int maxSoulCount = 3;
    [SerializeField] private Transform mouthTransform;
    [SerializeField] private string[] availableCharacters = { "Common", "Uncommon", "Rare" };

    private int storedSoulCount;

    public void FeedSoul(SoulBlock block)
    {
        if (block == null)
            return;

        storedSoulCount++;
        Destroy(block.gameObject);

        if (storedSoulCount >= maxSoulCount)
        {
            SpawnRandomCharacter();
            storedSoulCount = 0;
        }
    }

    private void SpawnRandomCharacter()
    {
        string character = availableCharacters[Random.Range(0, availableCharacters.Length)];
        Debug.Log("Dragon spat out a " + character + " character.");
    }
}
