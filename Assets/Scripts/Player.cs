using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //this is way too long... 
    //further I would seperate player health, movement, ui, and shooting into seperate scripts 
    [Header("Player Movement")]
    public Rigidbody rb;
    //public float playerSpeed;
    public float rotSpeed = 100f;
    public float currSpeed = 5f;
    public float sprintSpeed = 5f;
    public StaminaController staminaController;
    public Vector2 movement;
    public bool sprint = false;
    float horiz;
    float vert;
    public InputSystemUIInputModule m; 
    //public InputSystem uiMod;
    [Header("Player Index")]
    [SerializeField]
    private int playerindex = 0;
    [SerializeField]
    private UnityEvent CheatTime;


    public Rect Twoplayer;
    public Rect Threeplayer;
    public Rect fourPlayer;

    [Header("Player Info")] 
    public int playerHealth = 100;
    public int playerAmmo = 20;
    public int playerGold = 0;
    public int PadKey;
    public  bool IsDead;
    public Camera myCam;
    public CinemachineCamera CinemachineCam;

    [Header("Player Stuff")]

    public GameObject RespawnPoint;
    public GameObject RevivePoint;
    public GameObject GearObj;
    public GameObject MarkerObj;
    public GameObject spawnPt;
    public GameObject bulletObj;
    public Transform barrel;
    public HUDManager hud;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 RotInput = Vector2.zero;
    public GameManager gm;
    public Animator animator;
    private float xRotation = 0f;
    Animator anim;
    public GameObject tpExitRoom;

    [Header("PlayerInventory")]
    public int Gear = 0;
    public List<bool> HasKey;

    [Header("Audio Stuff")]
    public AudioSource audio;
    public AudioClip Shoot;
    public AudioClip PickupGold;
    public AudioClip PickupGear;
    public AudioClip PickupAmmo;
    public AudioClip LoseItem;
    public AudioClip GetHurt;


    [Header("Bullet Stuff")]
    public float shootCooldown = 0.5f; // Time in seconds between shots
    private float lastShootTime = 0f;

    void Start()
    {
        staminaController = GetComponent<StaminaController>();
        rb = GetComponent<Rigidbody>();
        //anim = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();

        // rb.freezeRotation = true;
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        //OnMoveInput(moveInput.x, moveInput.y);
    }
    public void OnRotate(InputAction.CallbackContext ctx)
    {
        RotInput = ctx.ReadValue<Vector2>();
    }
    public void Drop()
    {
        if (Gear >= 1)
        {
            Instantiate(GearObj, spawnPt.transform.position, Quaternion.identity);
            UseItem();
        }
        else
        {
            hud.FlashGearError();
        }

    }

    public void ActivateCheat()
    {
        Gear = 999;
        playerAmmo = 999;
        playerHealth = 999;
        CheatTime.Invoke();
    }
    public void OnNoButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LeaveGame()
    {

        transform.position = tpExitRoom.transform.position;
    }

    void Update()
    {
        //dumb long Update
        if (gm.totalPlayers == 2)
        {
            myCam.rect = Twoplayer;
        }
        else if (gm.totalPlayers == 3)
        {
            myCam.rect = Threeplayer;
        }
        else if (gm.totalPlayers == 4)
        {
            myCam.rect = fourPlayer;
            Debug.Log("4 player mode");
        }

        if (IsDead)
        {
            playerHealth = 100;
            playerGold = 0;
            IsDead = false;

        }

        if (sprint == true)
        {
            Sprint();
        }
        else
        {
            staminaController.isSprinting = false;
            staminaController.UpdateStamina(0);
            currSpeed = 5f;
        }

        Vector3 forward = transform.forward; // get the direction the player is facing
        Vector3 right = transform.right; // Right direction for left and right movement

        // Use the input to determine the movement direction
        Vector3 moveDir = forward * moveInput.y + right * moveInput.x; // Adjust movement direction based on input

        // Normalize the movement vector and apply speed
        if (moveDir.magnitude > 1)
        {
            animator.SetBool("IsWalking", true);
            moveDir.Normalize();
        }
        else if(moveDir.magnitude == 0)
        {
            animator.SetBool("IsWalking", false);
        }

        // Update the player's velocity
        rb.linearVelocity = moveDir * currSpeed;

        DoLooking();
        // Rotate the player based on input
        

        //do I need this?
        if (RotInput == Vector2.zero) 
        {
            float rotationY = RotInput.x * rotSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationY, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        
    }
    public void Sprint()
    {

        if (staminaController.playerStamina > 5f)
        {

            staminaController.isSprinting = true;
            staminaController.Sprinting(); ;
            staminaController.UpdateStamina(1);
            currSpeed = 10f;
        }
        else if (staminaController.playerStamina <= 5f)
        {

            sprint = false;
            staminaController.isSprinting = false;
            staminaController.UpdateStamina(0);
            currSpeed = 5f;
        }
    }

    private void DoLooking()
    {
        Vector3 looking = RotInput;
        float lookX = looking.x * rotSpeed * Time.deltaTime;
        float lookY = looking.y * rotSpeed * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //only the cinemachine camera needs to rotate up and down
        CinemachineCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * lookX);
    }
    public int GetPlayerIndex()
    {
        return playerindex;
    }

    public void OnSprint()
    {
        sprint = !sprint;

    }
    public void OnMoveInput(float horiz, float vert)
    {
        this.vert = vert;
        this.horiz = horiz;

    }
    public void OnRevive()
    {
        transform.position = RevivePoint.transform.position;
    }

    public void OnShoot()
    {

        if (Time.time >= lastShootTime + shootCooldown)
        {
            if (playerAmmo > 0)
            {
                audio.clip = Shoot;
                audio.Play();

                Vector3 shootDirection = CinemachineCam.transform.forward; // Camera's forward direction
                Vector3 spawnPos = barrel.position; //new
                GameObject bullet = GameObject.Instantiate(bulletObj, spawnPos, Quaternion.identity);
                BulletController bulletController = bullet.GetComponent<BulletController>();
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();


                // Set bullet's velocity based on that direction
                bulletRB.linearVelocity = shootDirection * bulletController.speed;

                //bulletRB.linearVelocity = this.transform.forward * bulletController.speed;
                playerAmmo -= 1;

                // Update the last shoot time
                lastShootTime = Time.time;
            }
            else
            {
                //Debug.Log("Out of ammo!");
                hud.FlashAmmoError();
            }
        }
        else
        {
            Debug.Log("Shot is on cooldown.");
        } 
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "EnemyBullet")
        {
            audio.clip = GetHurt;
            audio.Play();
            TakeDamage();
        }
    }
    public void InteractPopUp(string popUpMessage)
    {
        hud.PopUp.text = popUpMessage;
    }
    public void TakeDamage()
    {
        //playAudio
        if(playerHealth <= 0)
        {

            transform.position = RespawnPoint.transform.position;
            //StartCoroutine(DeathPopUp());

            IsDead = true;

        }
        else
        {
            playerHealth -=5;
            //flahs red
        }
    }
    public void AddGear()
    {
        audio.clip = PickupGear;
        audio.Play();
        Gear += 1;
        hud.DisplayGears();
    }
    public void GetKey(int index)
    {
        audio.clip = PickupGold;
        audio.Play();
        HasKey[index] = true;
        hud.DisplayKey(index);
    }
    public void LoseKey(int index)
    {
        audio.clip = LoseItem;
        audio.Play();
        HasKey[index] = false;
        hud.SetKeyOff(index);
    }

    public void AddAmmo(int AmmoValue)
    {
        audio.clip = PickupAmmo;
        audio.Play();
        playerAmmo += AmmoValue;
        hud.DisplayAmmo();
    }
    public void AddGold(int goldValue)
    {

        audio.clip = PickupGold;
        audio.Play();
        playerGold += goldValue;
        gm.TotalAddGold(goldValue);
    }
    public void UseItem()
    {
        //update ui for inventory
        if(Gear > 0)
        {
            Gear -= 1;
            hud.DisplayGears();
        }
        else
        {
            hud.FlashGearError();
        }
    }
}
