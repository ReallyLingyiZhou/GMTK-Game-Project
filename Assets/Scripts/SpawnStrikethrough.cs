using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SpawnStrikethrough : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public List<Image> Strikethroughs;
    [Range(-20, 20)]
    public float MaxZRotation = 10;
    [Range(-20, 20)]
    public float MinZRotation = -10;
    public Color Color = new Color(100, 100, 100, 255);
    public bool RandomFlips = true;
    void Start()
    {
        var ImageIn = Strikethroughs[Random.Range(0, Strikethroughs.Count)];
        var image = Duplicate(ImageIn);
        var curWidth = image.rectTransform.rect.size.x;
        var curScale = image.transform.localScale;
        var preferredWidth = textMeshPro.preferredWidth;
        image.transform.localScale = new Vector3((RandomFlips ? -1 : 1) * curScale.x * preferredWidth / curWidth, curScale.y, curScale.x);
        image.transform.localPosition = new Vector3(preferredWidth / 2, 0, 0);

        var curRotation = image.transform.localRotation;
        image.transform.localRotation = Quaternion.Euler(0, 0, curRotation.z + Random.Range(MinZRotation, MaxZRotation));
        textMeshPro.color = Color;
    }

    private Image Duplicate(Image ImageIn)
    {
        var image = Instantiate(ImageIn, textMeshPro.transform);
        image.SetNativeSize();
        return image;
    }
}
