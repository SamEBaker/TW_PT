using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Targets : MonoBehaviour
{

    public TargetManager _targetmanager;
    public AudioClip Click;
    public AudioSource audio;

    // Start is called before the first frame update
    public void Clear()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            audio.clip = Click;
            audio.Play();
            gameObject.SetActive(false);
            _targetmanager.HitTarget();
        }
    }
}
