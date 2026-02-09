using UnityEngine;

namespace Assets.Scripts
{
    public class FieldOfView : MonoBehaviour
    {

        // Use this for initialization
        private void Start()
        {
            Mesh mesh = new();
            GetComponent<MeshFilter>().mesh = mesh;

            float fov = 90f;
            _ = Vector3.zero;
            int rayCount = 2;
            _ = fov / rayCount;
            _ = new Vector3[rayCount + 1 + 1];

        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}