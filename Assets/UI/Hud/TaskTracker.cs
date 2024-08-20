using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskTracker : MonoBehaviour
{
    [SerializeField] private List<SpawnStrikethrough> strikethroughs;
    public TaskSummery GetAllTaskSummery()
    {
        return new TaskSummery()
        {
            time = Time.timeSinceLevelLoad,
            ObjectivesCompletedCount = CompletedCount,
            ObjectivesDestroyedCount = DestroyedCount,//strikethroughcount-completedcount
            TotalObjectives = strikethroughs.Count(),
            DestroyedCount = DestroyedCount
        };
    }
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

    // public void Complete(GameObject gameObject)
    // {
    //     CompletedCount++;
    //     var matched = Objectives.Find(obj => obj.ObjectiveName == gameObject.name);
    //     matched.TryDeactivate();
    // }

    // public void Destroy(GameObject gameObject)
    // {
    //     int id = gameObject.GetInstanceID();
    //     DestroyedCount++;
    //     var matched = Objectives.Find(obj => obj.ObjectiveName == gameObject.name);
    //     matched.TryDeactivate();
    // }
    public void MarkDestroyed()
    {
        DestroyedCount++;
    }

    public void completeObjective(int index, bool succeed = true)
    {
        if (succeed)
            CompletedCount++;
        else
            DestroyedCount++;
        strikethroughs[index].taskFinished = true;
        strikethroughs[index].Spawn();
    }
}
