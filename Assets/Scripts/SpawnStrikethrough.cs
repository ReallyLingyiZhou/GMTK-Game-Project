using System.Collections.Generic;
//using Palmmedia.ReportGenerator.Core.Logging;
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

    public int maxiStrike = 5; 
    public int currentStrike = 0;
    public bool taskFinished = false; 

    void Start()
    {
        // Spawn();
    }
    public void Spawn()
    {
        if(!taskFinished){
            return;
        }

        if(currentStrike >= maxiStrike){
            return;
        }

        var ImageIn = Strikethroughs[Random.Range(0, Strikethroughs.Count)];
        var image = Duplicate(ImageIn);
        var curWidth = image.rectTransform.rect.size.x;
        var curScale = image.transform.localScale;
        var preferredWidth = textMeshPro.preferredWidth;
        // image.transform.localScale = new Vector3((RandomFlips ? -1 : 1) * curScale.x * preferredWidth / curWidth, curScale.y, curScale.x);
        
        var curRotation = image.transform.localRotation;
        image.transform.localRotation = Quaternion.Euler(0, 0, curRotation.z + Random.Range(MinZRotation, MaxZRotation));
        textMeshPro.color = Color;
        //set rect transform posx to 118f
        image.rectTransform.localPosition = new Vector3(0, Random.Range(-15, 15), 0);
        
        currentStrike++;

    }

    private Image Duplicate(Image ImageIn)
    {
        Debug.Log(textMeshPro.transform);
        var image = Instantiate(ImageIn, textMeshPro.transform);
        image.SetNativeSize();
        return image;
    }
}
