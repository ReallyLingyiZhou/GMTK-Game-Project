using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(SpawnStrikethrough))]
public class Objective : MonoBehaviour
{
    [SerializeField, HideInInspector] public string Description;
    public string ObjectiveName;
    [SerializeField] private bool IsActive = true;
    [SerializeField, HideInInspector] TextMeshProUGUI textMeshPro;
    [SerializeField, HideInInspector] SpawnStrikethrough spawnStrikethrough;
    void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        spawnStrikethrough = gameObject.GetComponent<SpawnStrikethrough>();
        Description = textMeshPro.text;
    }

    public bool TryDeactivate()
    {
        if (!IsActive) return false;

        spawnStrikethrough.Spawn();
        IsActive = false;
        return true;
    }
}

