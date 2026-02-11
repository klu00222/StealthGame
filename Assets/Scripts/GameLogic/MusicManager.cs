using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource backgroundMusic;
    [SerializeField]
    private AudioSource chaseMusic;

    private void OnEnable()
    {
        ChaseBehaviour.OnChasingChange += SetChaseMusic;
    }

    private void OnDisable()
    {
        ChaseBehaviour.OnChasingChange -= SetChaseMusic;
    }

    private void SetChaseMusic(bool state)
    {
        if (state)
        {
            chaseMusic.Play();
            backgroundMusic.Stop();
        }
        else
        {
            chaseMusic.Stop();
            backgroundMusic.Play();
        }
    }
}
