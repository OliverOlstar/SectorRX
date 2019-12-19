using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballHitbox : MonoBehaviour
{
    public int damageAmount;
    public float maxTime;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > maxTime)
        {
            timer = 0;
            DestroyFireball();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerAttributes>() != null)
        {
            other.gameObject.GetComponent<PlayerAttributes>().TakeDamage(damageAmount, true);
            DestroyFireball();
        }
    }

    public void DestroyFireball()
    {
        Destroy(this.gameObject);
    }
}
