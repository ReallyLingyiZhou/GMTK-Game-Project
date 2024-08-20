using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StampDoc : MonoBehaviour
{
    public GameObject endMenu;
    public List<Image> good;
    public List<Image> neutral;
    public List<Image> bad;
    [Range(.01f, 1f)]
    public float GoodPercentageThreshold = .66f;
    [Range(.01f, 1f)]
    public float NeutralPercentageThreshold = .33f;
    [Range(1, 10000)]
    public int ScoreMult = 1000;
    [Range(0, 20)]
    public int RandRotation;
    void OnEnable()
    {
        RevealStamp();
    }
    private void RevealStamp()
    {
        var scorePercent = endMenu.GetComponent<EndMenu>().GetScorePercentage();
        Image image;
        if (scorePercent > GoodPercentageThreshold)
        {
            image = good[Random.Range(0, good.Count)];
        }
        else if (scorePercent > GoodPercentageThreshold)
        {
            image = neutral[Random.Range(0, neutral.Count)];
        }
        else
        {
            image = bad[Random.Range(0, bad.Count)];
        }

        image.gameObject.SetActive(true);
        var curRotation = image.transform.localRotation;
        image.transform.localRotation = Quaternion.Euler(curRotation.x, curRotation.y, curRotation.z + Random.Range(-RandRotation / 2, RandRotation / 2));
    }
}
