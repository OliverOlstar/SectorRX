using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 1.0f;
    
    private Vector3 distance;
    private float innerAngle;
    private float outerAngle;
    
    private float angleRadiansComponent;
    private float rightComponent;
    private float forwardComponent;
    private Vector3 move = Vector3.zero;
    
    public bool clockwise = false;

    public int count = 0;

    public bool bossAggro = false;
    
    // Update is called once per frame
    void Update()
    {       
        if(bossAggro)
        {
            transform.LookAt(player.transform);
            CalculateDirection();
            Straffe();
        }      
    }
    
    private void CalculateDirection()
    {        
        //get vector between boss and player
        distance = player.transform.position - transform.position;            

        //get angle to move, adjustable
        innerAngle = 90 * (1 / (distance.magnitude * 1.0f));

        //determine and to use trig on
        outerAngle = (180 - innerAngle) / 2;

        //trig it up to find new vector        
        angleRadiansComponent = (90 - (180 - innerAngle) / 2) * (Mathf.PI / 180);

        //multiplying by 1 like getting normalized vector
        rightComponent = 1 * Mathf.Cos(angleRadiansComponent);
        forwardComponent = 1 * Mathf.Sin(angleRadiansComponent);

        if(clockwise)
        {
            move = (transform.forward * forwardComponent) + -1 * (transform.right * rightComponent);
        }
        else
        {
            move = (transform.forward * forwardComponent) + (transform.right * rightComponent);
        }
            
        //get normal vector
        //move = move.normalized;
    }

    private void Straffe()
    {
        transform.position += move * speed * Time.deltaTime;
    }
    
    //random direction change
    IEnumerator SwitchDirection()
    {
        while(true)
        {
            //repeat every 3 seconds
            yield return new WaitForSecondsRealtime(3.0f);

            //get random number 0 or 1
            int x = Random.Range(0, 2);
            Debug.Log(x);

            //on 0 switch boss direction
            if (x == 1)
            {
                clockwise = !clockwise;
            }
        }        
    }
    
    private void OnCollisionEnter(Collision collision)
    {        
        //all the shit
        if(collision.gameObject.layer == 10 ||
            collision.gameObject.layer == 11 ||
            collision.gameObject.layer == 12 ||
            collision.gameObject.layer == 13)
        {            
            //reverse clockwise state
            clockwise = !clockwise;            
        }
    }
}
