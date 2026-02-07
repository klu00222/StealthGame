using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    public void Update()
    {
        if ((Keyboard.current != null) && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Quit();
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
               UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
