using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyBehavior : MonoBehaviour

//https://www.youtube.com/watch?v=UjkSFoLxesw&t=278s
{
    [Header("Enemy AI")]

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, whatisPlayer;
    public Vector3 walkPt;
    bool walkPtSet;
    public float walkPtRange;
    public float timeBtxAttack;
    bool Attacked;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public Animator anim;
    //public TargetRange tr;

    public GameObject Bullet;

    [Header("Enemy Stats")]
    public int enemyHealth;
    //public GameManager gm;

    private AudioSource audio;
    public AudioClip Damaged;
    public GameObject BulletSpawn;
    public IObjectPool<EnemyBehavior> Pool { get; set; }


    public void Awake()
    {
        //player = tr.target;
        //player = tr.p.transform;
        audio = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        //change to colliders
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);
        //player = tr.target;

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange)  Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patroling()
    {
        anim.SetBool("IsRunning", true);
        if (!walkPtSet) SearchWalkPt();
        if (walkPtSet)
        {
            agent.SetDestination(walkPt);
        }
        Vector3 distanceToWalk = transform.position - walkPt;

        if(distanceToWalk.magnitude < 1f)
        {
            walkPtSet = false;
        }
    }
    private void SearchWalkPt()
    {

        float randZ = Random.Range(-walkPtRange, walkPtRange);
        float randX = Random.Range(-walkPtRange, walkPtRange);

        walkPt = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
        if(Physics.Raycast(walkPt, -transform.up, 2f, Ground))
        {
            walkPtSet = true;
        }
    }

    private void Attacking()
    {
        Debug.Log("Attack!");
        agent.SetDestination(transform.position);
        anim.SetBool("IsRunning", false);
        transform.LookAt(player);

        if(!Attacked)
        {
            Rigidbody rb = Instantiate(Bullet, BulletSpawn.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 40f, ForceMode.Impulse);

            Attacked = true;
            Invoke(nameof(ResetAttack), timeBtxAttack);
        }
    }
    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void ResetAttack()
    {
        Attacked = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            audio.clip = Damaged;
            audio.Play();
            TakingDamage();
        }
    }
    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    public void TakingDamage()  
    {
        //playAudio
        if(enemyHealth <= 0)
        {
            //gm.TotalAddGold(5);
            ReturnToPool();
        }
        else
        {
            enemyHealth -= 10;
        }

    }
}

