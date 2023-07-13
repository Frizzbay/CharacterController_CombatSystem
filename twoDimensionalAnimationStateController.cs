using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDimensionalAnimationStateController : MonoBehaviour
{
    Animator animator;

    float Vertical = 0.0f;
    float Horizontal = 0.0f;
    public float acceleration = 1.0f;
    public float deceleration = 1.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 1.0f;


    // Increase performance 

    int VerticalHash;
    int HorizontalHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // Increase performance 
        VerticalHash = Animator.StringToHash("Velocity Z");
        HorizontalHash = Animator.StringToHash("Velocity X");
    }

    void changeVelocity(bool forwardPressed, bool backwardsPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        //////////////////////IF PRESSING //////////////////////
        if (forwardPressed && Vertical < currentMaxVelocity)
        {
            Vertical += Time.deltaTime * acceleration;
        }

        if (backwardsPressed && Vertical > -maximumWalkVelocity)
        {

            Vertical -= Time.deltaTime * acceleration;
            
        }

       // if (leftPressed && velocityX > -currentMaxVelocity)
       // {
       //     velocityX -= Time.deltaTime * acceleration;
      //  }

       // if (rightPressed && velocityX < currentMaxVelocity)
      //  {
      //      velocityX += Time.deltaTime * acceleration;
      //  }

        /////////////////////NOT PRESSING////////////////////

        if (!forwardPressed && Vertical > 0.0f)
        {
            Vertical -= Time.deltaTime * deceleration;
        }

        if (!backwardsPressed && Vertical < 0.0f)
        {
            Vertical += Time.deltaTime * deceleration;
        }

     //   if (!leftPressed && velocityX < 0.0f)
     //   {
     //       velocityX += Time.deltaTime * deceleration;
     //   }

       // if (!rightPressed && velocityX > 0.0f)
      //  {
      //      velocityX -= Time.deltaTime * deceleration;
     //   }
    }

    void lockOrResetVelocity(bool forwardPressed, bool backwardsPressed,  bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) 
    
    {
        if (!leftPressed && !rightPressed && Horizontal != 0.0f && (Horizontal > -0.05f && Horizontal < 0.05f))
        {
            Horizontal = 0.0f;
        }

        //lock forward
        if (forwardPressed && runPressed && Vertical > currentMaxVelocity)
        {
            Vertical = currentMaxVelocity;
        }
        else if (forwardPressed && Vertical > currentMaxVelocity)
        {
            Vertical -= Time.deltaTime * deceleration;
            // round within offset 
            if (Vertical > currentMaxVelocity && Vertical < (currentMaxVelocity + 0.05))
            {
                Vertical = currentMaxVelocity;
            }
        }
        // round within offset 
        else if (forwardPressed && Vertical < currentMaxVelocity && Vertical > (currentMaxVelocity - 0.05f))
        {
            Vertical = currentMaxVelocity;
        }

        //lock right
       // if (rightPressed && runPressed && velocityX > currentMaxVelocity)
      //  {
     //       velocityX = currentMaxVelocity;
    //    }
      //  else if (rightPressed && velocityX > currentMaxVelocity)
    //    {
      //      velocityX -= Time.deltaTime * deceleration;
            // round within offset 
      //      if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05))
     ////       {
     //           velocityX = currentMaxVelocity;
     //       }
   //     }
        // round within offset 
      //  else if (rightPressed && velocityZ < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
      //  {
     //       velocityX = currentMaxVelocity;
     //   }

        //lock left
      //  if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
     //   {
     //       velocityX = -currentMaxVelocity;
    //    }
     //   else if (leftPressed && velocityX < -currentMaxVelocity)
     //   {
      //      velocityX += Time.deltaTime * deceleration;
      //      // round within offset 
      //      if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05))
        //    {
        //        velocityX = -currentMaxVelocity;
       //     }
      //  }
        // round within offset 
       // else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
     //   {
     //       velocityX = -currentMaxVelocity;
     //   }
    }
    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardsPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);


        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;


        changeVelocity(forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);














        animator.SetFloat(VerticalHash, Vertical);
        animator.SetFloat(HorizontalHash, Horizontal);
    }
}
