using TMPro;
using UnityEngine;

public class MajorItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI majorName, majorDes;
    public void SetText(string name, string des)
    {
        majorName.text = name;
        majorDes.text = des;
        this.gameObject.SetActive(true);
    }
}
