using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armAnimation : MonoBehaviour
{
    PlayerMovement playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    public void armAnim() //plays animation
    {
        gameObject.GetComponent<Animator>().SetBool("swing", false);
        playerScript.canSwing = true;
    }
}
