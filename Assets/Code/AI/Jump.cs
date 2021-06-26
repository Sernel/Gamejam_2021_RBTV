using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;



public class Jump : MonoBehaviour
{
    bool jumping = false;
    public float thrust;
    private float maxJumpPoint;
    private bool doubleJumpLocked = false;
    
  
    void Start()
    {
        jumping = false;
        maxJumpPoint = this.gameObject.transform.position.y + 2;
    }

 
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            HandleInput((true));
        }
        else if(Input.GetKeyUp(("space")))
        {
            HandleInput(false);
        }
        
        if (jumping)
        {
            float gravity = this.gameObject.GetComponent<Rigidbody2D>().mass;

            if (maxJumpPoint - this.gameObject.transform.position.y > 0)
            {
                 this.gameObject.GetComponent<Rigidbody2D>().gravityScale =
                                gravity * ((maxJumpPoint - this.gameObject.transform.position.y )/100);
            }
            else
            {
                jumping = false;
                doubleJumpLocked = true;
            }
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 
                1 + ((maxJumpPoint - this.gameObject.transform.position.y )/100);

            if (this.gameObject.transform.position.y < maxJumpPoint - 1.8)
                doubleJumpLocked = false;
        }
    }
    
    private void HandleInput(bool spaceState)
    {
        if (spaceState && !jumping && !doubleJumpLocked)
        {
            jumping = true;
            maxJumpPoint = this.gameObject.transform.position.y + 2;
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);
            doubleJumpLocked = true;
        }

        if (!spaceState && jumping)
        {
            jumping = false;
        }

    }
}
