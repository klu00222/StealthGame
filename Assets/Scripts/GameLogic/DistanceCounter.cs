using TMPro;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI distanceText;

    private Vector3 position;
    private float distanceTravelled;

    private void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, position);

        distanceTravelled += distance;
        position = transform.position;

        distanceText.text = distanceTravelled.ToString("F0") + " m";
    }
}
