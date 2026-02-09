using UnityEngine;
using TMPro;

public class BestTimeTextUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI BestTimeText;

    public void Start()
    {
        TimeManager.Instance.ShowBestTimeText(BestTimeText);
    }
}
