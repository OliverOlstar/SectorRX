using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateBehaviour : StateMachineBehaviour
{

   public bool WaitVisual = false;

   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        WaitVisual = true;
    }
}
