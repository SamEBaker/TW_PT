using UnityEngine;

public class AttackRange : MonoBehaviour
{

    public EnemyBehavior e;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            e.playerInAttackRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        e.playerInAttackRange = false;
    }
}
