using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFalse()
    {
        GetComponent<Animator>().SetBool("idle", false);
        GetComponent<Animator>().SetBool("run", false);
        GetComponent<Animator>().SetBool("jump", false);
        GetComponent<Animator>().SetBool("shoot", false);
        GetComponent<Animator>().SetBool("dance", false);
        GetComponent<Animator>().SetBool("death", false);
    }

    public void SetTrue(string _string)
    {
        GetComponent<Animator>().SetBool(_string, true);
    }
}
