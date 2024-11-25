using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtonBehavior : MonoBehaviour
{

    public int ButtonID;
    public MeshRenderer mr;
    public Material norm;
    public Material pressed;
    public CheckButton c;
    public AudioSource audio;
    public AudioClip Click;
    public AudioClip Lose;

    public void Start()
    {
        mr.material = norm;
    }
    public void resetMaterial()
    {
        audio.clip = Lose;
        audio.Play();
        mr.material = norm;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audio.clip = Click;
            audio.Play();
            mr.material = pressed;
            c.AddtoOrder(ButtonID);
        }
    }

    

}
