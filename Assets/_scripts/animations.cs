using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animations : MonoBehaviour
{
    [SerializeField] Animator anim;

    private static int walk = Animator.StringToHash("isGoing");
    private static int start = Animator.StringToHash("isStarting");

    public void isGoing(bool state)
    {
        anim.SetBool(walk, state);
    }

    public void isStarting(bool state)
    {
        anim.SetBool(start, state);
    }
}
