using UnityEngine;


public class LockSpriteRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    private void Awake()
    {
        initialRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = initialRotation;
    }
}
