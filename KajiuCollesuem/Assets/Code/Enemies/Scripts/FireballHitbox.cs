using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballHitbox : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float maxTime;
    [SerializeField] private float knockForce;

    private void Start()
    {
        StartCoroutine("DestroyDelay");
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(maxTime);
        DestroyFireball();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IAttributes otherAttributes = other.gameObject.GetComponent<PlayerAttributes>();
            if (otherAttributes == null && other.transform.parent != null)
                otherAttributes = other.transform.parent.GetComponent<PlayerAttributes>();

            if (otherAttributes != null && otherAttributes.IsDead() == false)
            {
                otherAttributes.TakeDamage(damageAmount, GetComponent<Rigidbody>().velocity.normalized * knockForce, this.gameObject);
                DestroyFireball();
            }
        }

        if (!other.CompareTag("Enemy"))
        {
            DestroyFireball();
        }
    }

    public void DestroyFireball()
    {
        Destroy(this.gameObject);
    }
}
