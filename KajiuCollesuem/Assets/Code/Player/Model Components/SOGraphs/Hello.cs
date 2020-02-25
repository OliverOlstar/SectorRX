using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello : MonoBehaviour
{
    public float stepping;
    [SerializeField] private float stepspeed;
    private Animator anim;
    public SOGraph graph;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //stepping += Time.deltaTime / stepspeed;

        // Increase Stepping Animation
        stepping = increaseProgress(stepping, stepspeed);

        float secondStep = (stepping <= 0.5f) ? 0 : 0.5f;
        float time = (stepping - secondStep) * 2;

        //float steppingSpeed = _modelController.horizontalVelocity.magnitude / GetComponentInParent<MovementComponent>().maxSpeed;

        // Set Anim Stepping values
        anim.SetFloat("Stepping Progress", GetCatmullRomPosition(time, graph).y + secondStep);
    }

    private float increaseProgress(float pProgress, float pMult)
    {
        pProgress += Time.fixedDeltaTime * pMult;
        if (pProgress >= 1)
            pProgress -= 1;

        return pProgress;
    }

    public Vector2 GetCatmullRomPosition(float pTime, SOGraph pGraph)
    {
        Vector2 startPoint = Vector2.zero;
        Vector2 endPoint = new Vector2(1, pGraph.EndValue);

        Vector2 a = 2f * startPoint;
        Vector2 b = endPoint - pGraph.firstBender;
        Vector2 c = 2f * pGraph.firstBender - 5f * startPoint + 4f * endPoint - pGraph.secondBender;
        Vector2 d = -pGraph.firstBender + 3f * startPoint - 3f * endPoint + pGraph.secondBender;

        Vector2 pos = 0.5f * (a + (b * pTime) + (c * pTime * pTime) + (d * pTime * pTime * pTime));

        return pos;
    }
}
