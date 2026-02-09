using UnityEngine;
using TMPro;

public class BestTimeTextUI : MonoBehaviour
{
    public TextMeshProUGUI BestTimeText;

    public void Start()
    {
        TimeManager.Instance.ShowBestTimeText(BestTimeText);
    }
}
