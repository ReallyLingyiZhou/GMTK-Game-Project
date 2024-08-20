using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RotateDial : MonoBehaviour
{
    public GameObject endMenu;
    [SerializeField] private Image image;
    void OnEnable()
    {
        var breakageDegrees = endMenu.GetComponent<EndMenu>().GetBreakagePercentage() * 225;
        var curRotation = image.transform.localRotation;
        image.transform.localRotation = Quaternion.Euler(curRotation.x, curRotation.y, curRotation.z - breakageDegrees);
    }
}
