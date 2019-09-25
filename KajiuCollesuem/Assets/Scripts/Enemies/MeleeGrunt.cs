using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeGrunt : AI
{
    bool isDash = false;
    private Rigidbody rb;
    public int chargeHeight, chargeDistance, dashLength;

    // Start is called before the first frame update
    void Start()
    {
        //When enemy dashes, temporarily disable nav mesh agent, then dash forward, then turn on
        InheritStart();
        agent.stoppingDistance = 3;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mugiesshan, checks distance between grunt and player, and makes quick decision to dash or not

        if (Vector3.Distance(this.transform.position, player.transform.position) < 4 && isDash == false)
        {
            //isDash = Random.Range(0, 1) == 1 ? true : false;
            StartCoroutine( DashAttack());
        }

        InheritUpdate();
    }

    IEnumerator DashAttack()
    {
        //Mugiesshan, checks to see if enemy axis is facing player before dashing
        Vector3 dirFromEnToPlayer = (player.transform.position - this.transform.position).normalized;
        float inSight = Vector3.Dot(dirFromEnToPlayer, this.transform.forward);

        while (inSight < 0.9)
        {
            transform.LookAt(player.transform.position);
            yield return null;

            dirFromEnToPlayer = (player.transform.position - this.transform.position).normalized;
            inSight = Vector3.Dot(dirFromEnToPlayer, this.transform.forward);
        }

        isDash = true;

        //if (inSight > 0.9)
        //{
            Vector3 forward = transform.forward * chargeDistance;
            forward.y = chargeHeight;
            agent.enabled = false;

        yield return null;

            rb.AddForce(forward, ForceMode.Impulse);
            StartCoroutine(dashDelay());
        //}
    }

    IEnumerator dashDelay ()
    {
        yield return new WaitForSeconds(dashLength);
        Debug.Log("c");
        isDash = false;
        agent.enabled = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player") && isDash)
        {
            player.GetComponent<IAttributes>().TakeDamage(10, true);
        }
    }
}
