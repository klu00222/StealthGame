using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource ChaseMusic;

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
            ChaseMusic.Play();
            BackgroundMusic.Stop();
        }
        else
        {
            ChaseMusic.Stop();
            BackgroundMusic.Play();
        }
    }
}
