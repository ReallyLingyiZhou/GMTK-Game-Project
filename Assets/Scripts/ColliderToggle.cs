using UnityEngine;

public class ColliderToggleWithStringInput : MonoBehaviour
{
    private Collider targetCollider; // The collider on the same GameObject
    public string inputKeyString = "Mouse0"; // Public string to type the key name (e.g., "Mouse0" for left click, "Mouse1" for right click, "LeftShift" for Shift)

    private KeyCode inputKey;

    void Start()
    {
        // Automatically find the collider on the same GameObject
        targetCollider = GetComponent<Collider>();

        if (targetCollider != null)
        {
            targetCollider.enabled = false; // Start with the collider disabled
        }
        else
        {
            Debug.LogError("No Collider found on this GameObject!");
        }

        // Convert the string input to KeyCode
        if (!System.Enum.TryParse(inputKeyString, true, out inputKey))
        {
            Debug.LogError("Invalid key name! Please enter a valid KeyCode string.");
        }
    }

    void Update()
    {
        if (targetCollider != null && inputKey != KeyCode.None)
        {
            if (Input.GetKey(inputKey))
            {
                EnableCollider();
            }
            else
            {
                DisableCollider();
            }
        }
    }

    private void EnableCollider()
    {
        if (!targetCollider.enabled)
        {
            targetCollider.enabled = true;
        }
    }

    private void DisableCollider()
    {
        if (targetCollider.enabled)
        {
            targetCollider.enabled = false;
        }
    }
}
