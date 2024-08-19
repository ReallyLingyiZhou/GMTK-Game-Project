using UnityEngine;

public class RandomSoundOnDestroy : MonoBehaviour
{
    // Array of possible sound effects
    public AudioClip[] soundEffects;

    // Audio source component
    private AudioSource audioSource;

    // Volume control
    [Range(0f, 1f)]
    public float volume = 1.0f;

    void Start()
    {
        // Create or get an AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnDestroy()
    {
        // Check if there are any sounds available
        if (soundEffects.Length > 0)
        {
            // Select a random sound from the array
            AudioClip randomSound = soundEffects[Random.Range(0, soundEffects.Length)];

            // Play the selected sound with the specified volume
            audioSource.PlayOneShot(randomSound, volume);
        }
    }
}
