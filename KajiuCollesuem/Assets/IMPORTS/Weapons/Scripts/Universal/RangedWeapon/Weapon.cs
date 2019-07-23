using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Attributes")]
    public float _BulletSize = 0.3f;
    public int _MagSize = 20;
    public float _ShotForce = 10.0f;
    public float _FireRate = 1.0f;
    public float _FireCountdown = 0f;

    [Header("Weapon Parts")]
    public GameObject _Bullet;
    public GameObject _Magazine;
    public GameObject _MagazineContainer;
    public Transform _ShotSpawn;
    public BulletManager _Manager;


    [Header("Player Details")]
    public GameObject _Player;

    void Start()
    {
        CheckForBulletManager();
    }

    // Update is called once per frame
    void Update()
    {
        if(_FireCountdown <= 0)
        {
            Shoot();
        } else if(_FireCountdown > 0)
        {
            _FireCountdown -= Time.deltaTime;
        }

        

    }


    #region WeaponMechanics

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _FireCountdown = 1f / _FireRate;
            _Manager._ShotTriggered = true;
            

        }
    }

    #endregion


    #region WeaponSetup

    void CheckForBulletManager()
    {
        if(_Manager == null)
        {
            GameObject Manager = _Magazine;
            Manager.AddComponent<BulletManager>();

            _Manager = Manager.GetComponent<BulletManager>();

            SetManagerFields();
        }
        else
        {
            return;
        }
    }

    void SetManagerFields()
    {
        if(_Manager != null)
        {
            SetManagerListSize();
            SetManagerBulletPrefab();
            SetManagerShotForce();
            SetManagerShotSpawn();
            SetManagerPlayerObject();
            SetManagerBulletContainer();
            SetManagerBulletSize();
        }
        else
        {
            CheckForBulletManager();
        }
    }

    void SetManagerListSize()
    {
        _Manager.SetListSize(_MagSize);
    }

    void SetManagerBulletPrefab()
    {
        _Manager.SetBulletPrefab(_Bullet);
    }

    void SetManagerShotForce()
    {
        _Manager.SetShotForce(_ShotForce);
    }

    void SetManagerShotSpawn()
    {
        _Manager.SetShotSpawn(_ShotSpawn);
    }

    void SetManagerPlayerObject()
    {
        _Manager.SetPlayerObject(_Player);
    }

    void SetManagerBulletContainer()
    {
        _Manager.SetBulletContainer(_MagazineContainer);
    }

    void SetManagerBulletSize()
    {
        _Manager.SetBulletSize(_BulletSize);

    }

    #endregion
}
