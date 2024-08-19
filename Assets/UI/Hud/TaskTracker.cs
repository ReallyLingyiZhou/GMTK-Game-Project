using System.Collections.Generic;
using UnityEngine;

public class TaskTracker : MonoBehaviour
{
    [SerializeField] private List<Objective> Objectives;
    [SerializeField] private bool AllDone = false;
    [SerializeField] private int CompletedCount = 0;
    [SerializeField] private int DestroyedCount = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Complete(GameObject gameObject)
    {
        CompletedCount++;
        var matched = Objectives.Find(obj => obj.ObjectiveName == gameObject.name);
        matched.IsActive = false;
    }
    public void Destroy(GameObject gameObject)
    {
        int id = gameObject.GetInstanceID();
        DestroyedCount++;
        var matched = Objectives.Find(obj => obj.ObjectiveName == gameObject.name);
        matched.IsActive = false;
    }
}
