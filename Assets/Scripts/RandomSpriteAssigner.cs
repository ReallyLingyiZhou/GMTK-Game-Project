using UnityEngine;
using UnityEngine.UI;

public class RandomSpriteAssigner : MonoBehaviour
{
    // Array of possible sprites
    public Sprite[] sprites;

    // Reference to the Image component
    private Image image;

    void Start()
    {
        // Get the Image component attached to the same GameObject
        image = GetComponent<Image>();

        // Check if there are any sprites available in the array
        if (sprites.Length > 0 && image != null)
        {
            // Select a random sprite from the array
            Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];

            // Assign the random sprite to the Image component
            image.sprite = randomSprite;
        }
    }
}
