using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayerHolding()
    {
        anim.SetBool("Holding", true);
    }
    public void PlayerRelease()
    {
        anim.SetBool("Holding", false);
    }
}
