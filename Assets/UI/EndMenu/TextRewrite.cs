using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class TextRewrite : MonoBehaviour
{
    public GameObject endMenu;
    public TextMeshProUGUI textMeshPro;
    public List<string> good;
    public List<string> neutral;
    public List<string> bad;
    [Range(.01f, 1f)]
    public float GoodPercentageThreshold = .66f;
    [Range(.01f, 1f)]
    public float NeutralPercentageThreshold = .33f;
    [Range(1, 10000)]
    public int ScoreMult = 1000;
    [Range(0, 40)]
    public int RandRotation = 0;
    void OnEnable()
    {
        Rewrite();
    }
    private void Rewrite()
    {
        var scorePercent = endMenu.GetComponent<EndMenu>().GetScorePercentage();
        string text;
        if (scorePercent > GoodPercentageThreshold)
        {
            text = good[Random.Range(0, good.Count)];
        }
        else if (scorePercent > GoodPercentageThreshold)
        {
            text = neutral[Random.Range(0, neutral.Count)];
        }
        else
        {
            text = bad[Random.Range(0, bad.Count)];
        }

        textMeshPro.text = text;
        // textMeshPro.gameObject.SetActive(true);
        var curRotation = textMeshPro.transform.localRotation;
        textMeshPro.transform.localRotation = Quaternion.Euler(curRotation.x, curRotation.y, curRotation.z + Random.Range(-RandRotation / 2, RandRotation / 2));
    }
}
