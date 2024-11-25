using UnityEngine;

public class TargetRange : MonoBehaviour
{
    public Transform target;
    public EnemyBehavior e;

    public void Start()
    {
        target = GetComponent<Transform>();
    }

    private void Update()
    {
        if (!e.playerInSightRange)
        {
            e.player = null;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            e.playerInSightRange = true;
            e.player = other.gameObject.transform;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        e.playerInSightRange = false;
    }
}
