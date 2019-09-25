using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    static StatusEffects instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static IEnumerator Effect(State state, int seconds)
    {
        Debug.Log(state);
        yield return new WaitForSeconds(seconds);
        Debug.Log("Player is back to normal");
    }

    public static void Status(string state)
    {
        if (state == "burn")
        {
            //start burn corutine
            instance.StartCoroutine(Effect(State.Burn, 10));
        }
    }
}

public enum State
{
    Burn
}
