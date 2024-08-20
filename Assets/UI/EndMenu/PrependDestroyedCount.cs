using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrependDestroyedCount : MonoBehaviour
{
    public GameObject endMenu;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private void OnEnable()
    {
        var prepend = endMenu.GetComponent<TaskSummery>().DestroyedCount;
        Debug.Log(prepend.ToString());
        Debug.Log(textMeshPro.text);

        textMeshPro.text = prepend.ToString() + textMeshPro.text;
    }
}
