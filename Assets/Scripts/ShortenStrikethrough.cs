using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShortenStrikethrough : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Image image;
    public int maxText;
    // private Vector3 scaleChange, positionChange;
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.transform.localScale.x = gameObject.transform.localScale.x  * (float)textMeshPro.text.Length / (float)maxText;

        // gameObject.transform.localScale += scaleChange;
        // gameObject.transform.position += positionChange;

        // // Move upwards when the sphere hits the floor or downwards
        // // when the sphere scale extends 1.0f.
        // if (gameObject.transform.localScale.y < 0.1f || gameObject.transform.localScale.y > 1.0f)
        // {
        //     scaleChange = -scaleChange;
        //     positionChange = -positionChange;
        // }
        var curScale = image.transform.localScale;
        // var scaleMult = textMeshPro.preferredWidth;
        var scaleMult = textMeshPro.text.Length / maxText;
        image.transform.localScale = new Vector3(curScale.x * scaleMult, curScale.y, curScale.x);
    }
    // private float GetTextActualBounds(){
    //     // Text mesh size
    //     foreach (char symbol in textMeshPro.text)
    //     {
    //     CharacterInfo info;
    //     if (textMeshPro.font.(symbol, out info, textMeshPro.fontSize, textMeshPro.fontStyle))
    //     {
    //         width += info.advance;
    //     }
    //     }
    //     return width * textMesh.characterSize * textMesh.transform.lossyScale.x * 0.1f;
    // }
}
