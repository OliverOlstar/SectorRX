using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfieMovement : MonoBehaviour
{
    public Transform player;
    static Animator anim;

    [SerializeField]
    private float fireballDuration;
    public float fireballCooldown = 1.0f;
    public float newFireballTime = 0.0f;

    public Rigidbody fireballPrefab;
    public Transform FBSpawnpoint;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.position, this.transform.position) < 15.0f)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);



            if (Time.time > newFireballTime)
            {
                if (direction.magnitude < 10.0f)
                {
                    this.transform.Translate(0, 0, 0.1f);
                    // anim.SetBool("isRunning", true);
                    // anim.SetBool("isPunching", false);
                }

                else
                {

                    // anim.SetBool("isRunning", false);
                    // anim.SetBool("isPunching", true);
                    StartCoroutine(fireCast());

                }
            }

        }
        /*else
        {
            //anim.SetBool("isIdle", true);
            //anim.SetBool("isRunning", false);
            //anim.SetBool("isPunching", false);

            Rigidbody FireballInstance;

            FireballInstance = Instantiate(fireballPrefab, FBSpawnpoint.position, FBSpawnpoint.rotation) as Rigidbody;
            FireballInstance.AddForce(FBSpawnpoint.forward * 1000);
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Fireball")
        {
            Destroy(gameObject, 0.0f);
        }
    }

    IEnumerator fireCast()
    {
        
anim.SetTrigger("SummonComplete");
        //anim.SetBool("isIdle", false);
        //anim.SetBool("IsRunning", false);

        Rigidbody FireballInstance;

        FireballInstance = Instantiate(fireballPrefab, FBSpawnpoint.position, FBSpawnpoint.rotation) as Rigidbody;
        FireballInstance.AddForce(FBSpawnpoint.forward * 1000);

        yield return new WaitForSeconds(fireballDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newFireballTime = Time.time + fireballCooldown;
    }
}