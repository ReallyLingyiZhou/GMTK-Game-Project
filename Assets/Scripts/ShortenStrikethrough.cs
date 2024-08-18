using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShortenStrikethrough : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Image image;
    void Start()
    {
        var curWidth = image.rectTransform.rect.size.x;
        var curScale = image.transform.localScale;
        var preferredWidth = textMeshPro.preferredWidth;
        image.transform.localScale = new Vector3(curScale.x * preferredWidth / curWidth, curScale.y, curScale.x);
        image.transform.localPosition = new Vector3(preferredWidth / 2, 0, 0);
    }
}
