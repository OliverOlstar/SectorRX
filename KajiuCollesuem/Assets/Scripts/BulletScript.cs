using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    public float speed;
    public GameObject Player;
    public Vector3 direction;

    void Start()
    {
        direction = (Player.transform.position - transform.position);
        direction.y = 0;
        direction = direction.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerAttributes>().takeDamage(damage);
            Destroy(this.gameObject);
        }
        
    }
}
