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

    public float waitBetweenShots;
    private float shotCounter;

    [SerializeField]
    private float torpedoDuration;
    public float torpedoCooldown = 1.0f;
    public float newTorpedoTime = 0.0f;

    public float waitBetweenTorpedo;
    private float torpedoCounter;

    [SerializeField]
    private float biteDuration;
    public float biteCooldown = 2.0f;
    public float newBiteTime = 0.0f;

    public float waitBetweenBite;
    private float biteCounter;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        shotCounter = waitBetweenShots;
        torpedoCounter = waitBetweenTorpedo;
        biteCounter = waitBetweenBite;
    }

    // Update is called once per frame
    void Update()
    {
        shotCounter -= Time.deltaTime;
        torpedoCounter -= Time.deltaTime;
        biteCounter -= Time.deltaTime;

        if (Vector3.Distance(player.position, this.transform.position) < 20.0f)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);



            if (Time.time > newFireballTime)
            {
                if (direction.magnitude < 15.0f)
                {
                    this.transform.Translate(0, 0, 0.1f);
                    anim.SetBool("PlayerInRange", true);
                }

                else if (shotCounter < 0)
                {
                    anim.SetBool("PlayerInRange", false);
                    StartCoroutine(fireCast());
                    shotCounter = waitBetweenShots;
                }
            }

            else if (Time.time > newTorpedoTime)
            {
                if (direction.magnitude < 10.0f)
                {
                    this.transform.Translate(0, 0, 0.1f);
                    anim.SetBool("PlayerInRange", true);
                }

                else if (torpedoCounter < 0)
                {
                    anim.SetBool("PlayerInRange", false);
                    StartCoroutine(torpedoCast());
                    torpedoCounter = waitBetweenTorpedo;
                }
            }

            else if (Time.time > newBiteTime)
            {
                if (direction.magnitude < 5.0f)
                {
                    this.transform.Translate(0, 0, 0.1f);
                    anim.SetBool("PlayerInRange", true);

                }

                else if (biteCounter < 0)
                {
                    anim.SetBool("PlayerInRange", false);
                    StartCoroutine(biteCast());
                    biteCounter = waitBetweenBite;
                }
            }
        }
        else
        {
            anim.SetBool("PlayerInRange", false);
        }
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
        Rigidbody FireballInstance;

        FireballInstance = Instantiate(fireballPrefab, FBSpawnpoint.position, FBSpawnpoint.rotation) as Rigidbody;
        FireballInstance.AddForce(FBSpawnpoint.forward * 1000);

        yield return new WaitForSeconds(fireballDuration);
        anim.SetTrigger("FiringRange");

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newFireballTime = Time.time + fireballCooldown;
    }

    IEnumerator torpedoCast()
    {
        anim.SetBool("TargetLongRange", true);
        yield return new WaitForSeconds(torpedoDuration);
        anim.SetBool("TargetLongRange", false);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newTorpedoTime = Time.time + torpedoCooldown;
    }

    IEnumerator biteCast()
    {
        anim.SetBool("TargetCloseRange", true);
        yield return new WaitForSeconds(biteDuration);
        anim.SetBool("TargetCloseRange", false);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newBiteTime = Time.time + biteCooldown;
    }
}