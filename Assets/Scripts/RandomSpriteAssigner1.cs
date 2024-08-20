using UnityEngine;

public class RandomSpriteAssigner1 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite[] sprites; // Array of sprites to choose from

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); // Try to get the SpriteRenderer from the same GameObject
        }

        if (spriteRenderer != null && sprites.Length > 0)
        {
            AssignRandomSprite();
        }
        else
        {
            Debug.LogError("SpriteRenderer not assigned or no sprites available in the array!");
        }
    }

    private void AssignRandomSprite()
    {
        int randomIndex = Random.Range(0, sprites.Length); // Get a random index
        spriteRenderer.sprite = sprites[randomIndex]; // Assign the random sprite to the SpriteRenderer
    }
}
