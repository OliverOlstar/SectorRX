using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scor : MonoBehaviour
{
    private int Score = 0;
    private Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore(int pScore)
    {
        Score += pScore;
        txt.text = Score + "";
    }
}
