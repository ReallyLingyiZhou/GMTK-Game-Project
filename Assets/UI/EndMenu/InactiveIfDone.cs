using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class InactiveIfDone : MonoBehaviour
{
    public GameObject endMenu;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private void OnEnable()
    {
        var total = endMenu.GetComponent<EndMenu>().GetTotalObjectives();
        var done = endMenu.GetComponent<TaskSummery>().ObjectivesCompletedCount;
        Debug.Log("total" + total.ToString() + "" + done.ToString());
        if (total == done)
            textMeshPro.gameObject.SetActive(false);
    }
}
