using TMPro;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI DistanceText;
    Vector3 Position;
    float DistanceTravelled;


    private void Start()
    {
        Position = transform.position;
    }

    void Update()
    {
        float Distance = Vector3.Distance(transform.position, Position);

        DistanceTravelled += Distance;

        Position = transform.position;

        DistanceText.text = DistanceTravelled.ToString("F0") + " m";

        Debug.Log(transform.position);
    }
}
