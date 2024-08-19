using UnityEngine;
using TMPro;

public class RandomizeText : MonoBehaviour
{
    // Public array of possible texts to be set in the Unity Inspector
    public string[] possibleTexts;

    // Reference to the TextMeshPro component
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        // Get the TextMeshPro component attached to the same GameObject
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Check if there are any texts available in the array
        if (possibleTexts.Length > 0)
        {
            // Select a random text from the array
            string randomText = possibleTexts[Random.Range(0, possibleTexts.Length)];

            // Set the random text to the TextMeshPro component
            textMeshPro.text = randomText;
        }
    }
}
