using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballHitbox : MonoBehaviour
{
    public int damageAmount;
    public float maxTime;
    float timer = 0;

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
        IAttributes otherAttributes = other.gameObject.GetComponent<PlayerAttributes>();

        if (otherAttributes != null)
        {
            otherAttributes.TakeDamage(damageAmount, Vector3.zero, true);
            DestroyFireball();
        }
    }

    public void DestroyFireball()
    {
        Destroy(this.gameObject);
    }
}
