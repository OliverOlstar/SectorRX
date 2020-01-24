using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Description: Plasma Breath Ability for Lizzy Scale (Godzilla Character)*/ 

public class PlasmaBreath : MonoBehaviour
{
    //Variables for attack spawn
    public GameObject laserPrefab;
    public Transform playerPos;
    public Transform spawnPoint;
    public GameObject mainCam;
    public float chargeTimer;

    //Boolean to check if attacking or not
    public bool laserOn;
    public bool firstPersonOn;

    //GameObject to Instantiate
    private GameObject _SpawnedLaser;
    
    [SerializeField]
    private float _RotateDampening = 5.0f;

    //To access outside variables
    MovementComponent _PlayerMov;
    EnemyAttributes _EnemyTest;

    // Start is called before the first frame update
    void Start()
    {
        chargeTimer = 0;
        _SpawnedLaser = Instantiate(laserPrefab, spawnPoint.transform) as GameObject;
        _PlayerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementComponent>();
        //_EnemyTest = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAttributes>();
    }

    //Get a normalized horizontal Vector
    private Vector3 Horizontalize(Vector3 pVector)
    {
        pVector.y = 0;
        pVector.Normalize();
        return pVector;
    }

    // Update is called once per frame
    void Update()
    {   
        //If assigned key is pressed and not attacking, a timer starts to increase.
        if(Input.GetKey(KeyCode.Z) && laserOn == false)
        {
            _PlayerMov.disableMovement = true;
            chargeTimer += 0.6f * Time.deltaTime;
            playerPos.forward = Vector3.Slerp(Horizontalize(playerPos.forward), Horizontalize(mainCam.transform.forward), Time.deltaTime * _RotateDampening);
        }

        //If at anytime while charging and not attacking the key is no longer being pressed, timer resets to zero.
        if(Input.GetKeyUp(KeyCode.Z) && laserOn == false)
        {
            chargeTimer = 0;
            firstPersonOn = false;
        }

        //Once timer reaches 2, attacki begins, instantiating the beam particle with a hitbox.
        if (chargeTimer >= 2.0f)
        {
            EnableLaser();
            UpdateLaser();
            laserOn = true;
            firstPersonOn = true;
        }

        //If attacking is true, timer begins to automatically descrease. Player cannot move while executing attack.
        if(laserOn)
        {
            chargeTimer -= 0.6f * Time.deltaTime;
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _PlayerMov.transform.Rotate(0, 1.0f * 0.5f, 0);
            }
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _PlayerMov.transform.Rotate(0, -1.0f * 0.5f, 0);
            }
        }

        //Allows player to aim the beam while it is firing
        if(firstPersonOn)
        {
            _SpawnedLaser.transform.Rotate(Input.GetAxis("Mouse X") * 0.5f, 0, 0);
            _SpawnedLaser.transform.Rotate(0, Input.GetAxis("Mouse Y") * 0.5f, 0);
            playerPos.transform.eulerAngles = new Vector3(0, mainCam.transform.eulerAngles.y, 0);
            //playerPos.forward = Vector3.Slerp(Horizontalize(playerPos.forward), Horizontalize(mainCam.transform.forward), Time.deltaTime * _RotateDampening);
        }

        //When timer has counted down to zero, beam particle turns off. Timer resets to zero if value is lower. 
        if (chargeTimer <= 0)
        {
            DisableLaser();
            chargeTimer = 0;
            laserOn = false;
            firstPersonOn = false;
            _PlayerMov.disableMovement = false;
        }
    }

    //Sets laser prefab to being active.
    void EnableLaser()
    {
        _SpawnedLaser.SetActive(true);
    }

    //Updates the laser's position based on the direction the player is facing.
    void UpdateLaser()
    {
        if(spawnPoint != null)
        {
            spawnPoint.transform.localRotation = Quaternion.LookRotation(playerPos.forward);
            _SpawnedLaser.transform.position = spawnPoint.transform.position;
            _SpawnedLaser.transform.rotation = spawnPoint.transform.localRotation;
        }

        //If in first-person mode, laser beam is aimed depending on camera's rotation
        if (firstPersonOn)
        {
            _SpawnedLaser.transform.rotation = mainCam.transform.localRotation;
        }
    }

    //Sets laser prefab to being inactive.
    void DisableLaser()
    {
        _SpawnedLaser.SetActive(false);
    }

    //Enemies caught within the beam take damage.
    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag == "Enemy")
        //{
        //    Debug.Log("Enemy Blasted");
        //    _EnemyTest.TakeDamage(25, true);
        //}
    }
}
