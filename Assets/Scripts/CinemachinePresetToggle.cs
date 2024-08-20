using UnityEngine;
using Cinemachine;

public class CinemachinePresetToggleByChecklistDirect : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine Virtual Camera
    public GameObject checklist; // Direct reference to the Checklist GameObject

    // Preset A (values when the Checklist is active)
    public float screenX_A = 0.5f;
    public float deadZoneWidth_A = 0.1f;
    public float softZoneWidth_A = 0.8f;
    public float biasX_A = 0f;

    // Preset B (values when the Checklist is inactive)
    public float screenX_B = 0.3f;
    public float deadZoneWidth_B = 0.2f;
    public float softZoneWidth_B = 0.6f;
    public float biasX_B = 0.2f;

    public float lerpDuration = 1f; // Duration of the lerp

    private CinemachineFramingTransposer framingTransposer; // Reference to the Framing Transposer
    private bool isLerping = false;

    void Start()
    {
        if (virtualCamera != null)
        {
            framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            if (framingTransposer != null)
            {
                if (checklist != null)
                {
                    // Set initial values based on the Checklist state
                    ApplyCurrentPreset();
                }
                else
                {
                    Debug.LogError("No Checklist GameObject assigned!");
                }
            }
            else
            {
                Debug.LogError("No Framing Transposer found on the Cinemachine Virtual Camera!");
            }
        }
        else
        {
            Debug.LogError("No Cinemachine Virtual Camera assigned!");
        }
    }

    void Update()
    {
        if (checklist != null && framingTransposer != null && !isLerping)
        {
            ApplyCurrentPreset();
        }
    }

    private void ApplyCurrentPreset()
    {
        if (checklist.activeSelf)
        {
            StartLerp(screenX_A, deadZoneWidth_A, softZoneWidth_A, biasX_A);
        }
        else
        {
            StartLerp(screenX_B, deadZoneWidth_B, softZoneWidth_B, biasX_B);
        }
    }

    private void StartLerp(float targetScreenX, float targetDeadZoneWidth, float targetSoftZoneWidth, float targetBiasX)
    {
        if (!isLerping)
        {
            StopAllCoroutines();
            StartCoroutine(LerpToPreset(targetScreenX, targetDeadZoneWidth, targetSoftZoneWidth, targetBiasX));
        }
    }

    private System.Collections.IEnumerator LerpToPreset(float targetScreenX, float targetDeadZoneWidth, float targetSoftZoneWidth, float targetBiasX)
    {
        isLerping = true;
        float elapsedTime = 0f;

        float startScreenX = framingTransposer.m_ScreenX;
        float startDeadZoneWidth = framingTransposer.m_DeadZoneWidth;
        float startSoftZoneWidth = framingTransposer.m_SoftZoneWidth;
        float startBiasX = framingTransposer.m_BiasX;

        while (elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;

            framingTransposer.m_ScreenX = Mathf.Lerp(startScreenX, targetScreenX, elapsedTime / lerpDuration);
            framingTransposer.m_DeadZoneWidth = Mathf.Lerp(startDeadZoneWidth, targetDeadZoneWidth, elapsedTime / lerpDuration);
            framingTransposer.m_SoftZoneWidth = Mathf.Lerp(startSoftZoneWidth, targetSoftZoneWidth, elapsedTime / lerpDuration);
            framingTransposer.m_BiasX = Mathf.Lerp(startBiasX, targetBiasX, elapsedTime / lerpDuration);

            yield return null;
        }

        // Ensure the final values are exactly the target values
        framingTransposer.m_ScreenX = targetScreenX;
        framingTransposer.m_DeadZoneWidth = targetDeadZoneWidth;
        framingTransposer.m_SoftZoneWidth = targetSoftZoneWidth;
        framingTransposer.m_BiasX = targetBiasX;

        isLerping = false;
    }
}
