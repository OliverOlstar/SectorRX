using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Magazine Setup")]
    public List<GameObject> _Bullets = new List<GameObject>();
    public Transform _PlayerTransform;
    public GameObject _BulletContainer;
    public int _ReceivedMagSize;

    [Header("Bullet Setup")]
    public GameObject _BulletPrefab;
    public Vector3 _BulletSize;
    public float _ReceivedShotForce;
    public Transform _ShotSpawn;
    public GameObject _Player;

    public bool _ShotTriggered = false;

    private void Awake()
    {
        
    }


    void Start()
    {
        for (int i = 0; i < _ReceivedMagSize; i++)
        {
            GameObject bullet = Instantiate(_BulletPrefab, gameObject.transform);
            
            bullet.GetComponent<Bullet>()._ShotForce = _ReceivedShotForce;

            bullet.SetActive(false);

            _Bullets.Add(bullet);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForShotTrigger();
        _PlayerTransform = _Player.transform;

    }

    void CheckForShotTrigger()
    {
        if (_ShotTriggered)
        {
            for (int i = 0; i < _Bullets.Count; i++)
            {
                if (!_Bullets[i].activeSelf)
                {
                    GameObject bullet = _Bullets[i];
                    //_PlayerTransform = _Player.transform;
                    Quaternion rotation = Quaternion.Euler(0, _PlayerTransform.rotation.eulerAngles.y, 0);

                    bullet.transform.position = _ShotSpawn.position;
                    bullet.transform.rotation = rotation;

                    bullet.transform.parent = _BulletContainer.transform;
                    bullet.transform.localScale = _BulletSize;

                    
                    _Bullets[i].SetActive(true);
                    _ShotTriggered = false;
                    break;
                }
            }
        }
    }


    #region ManagerSetup

    public void SetListSize(int size)
    {
        _ReceivedMagSize = size;
    }

    public void SetBulletPrefab(GameObject obj)
    {
        _BulletPrefab = obj;
    }

    public void SetShotForce(float force)
    {
        _ReceivedShotForce = force;
    }

    public void SetShotSpawn(Transform transform)
    {
        _ShotSpawn = transform;
    }

    public void SetPlayerObject(GameObject obj)
    {
        _Player = obj;
    }

    public void SetBulletContainer(GameObject obj)
    {
        _BulletContainer = obj;
    }

    public void SetBulletSize(float size)
    {
        _BulletSize = new Vector3(size, size, size);
    }

    #endregion
}
