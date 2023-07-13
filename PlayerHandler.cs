using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [Range(0f, 20f)]
    public float rotationSpeed = 0f;

    public Transform Model;

    public Transform TargetLock;

    private Camera mainCamera;

    private Animator Anim;

    private Vector3 StickDirection;

    private bool IsWeaponEquipped = false;

    private bool IsTargetLocked = false;

    

    float strafeVertical = 0;
    float strafeHorizontal = 0;
    float speed = 0.0f; 
    public float acceleration = 1.0f;
    public float deceleration = 1.0f;
    public float strafeAcceleration = 1.0f;
    public float strafeDeceleration = 1.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 1.0f;
    //public float maximumStrafeVelocity = 0.5f;
    //Increase Performance
    
    int strafeVerticalHash;
    int strafeHorizontalHash;
    int speedHash;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Anim = Model.GetComponent<Animator>();

        speedHash = Animator.StringToHash("Speed");
        strafeVerticalHash = Animator.StringToHash("strafeVertical");
        strafeHorizontalHash = Animator.StringToHash("strafeHorizontal");

        
    }
    void changeStrafeVelocity(bool forwardPressed, bool backwardsPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        if (forwardPressed && strafeVertical < currentMaxVelocity)
        {
            strafeVertical += Time.deltaTime * strafeAcceleration;
        }
        if (backwardsPressed && strafeVertical > -currentMaxVelocity)
        {
            strafeVertical -= Time.deltaTime * strafeAcceleration;
        }
        if (leftPressed && strafeHorizontal > -currentMaxVelocity)             
        {
            strafeHorizontal -= Time.deltaTime * strafeAcceleration;
        }
        if (rightPressed && strafeHorizontal < currentMaxVelocity)
        {
            strafeHorizontal += Time.deltaTime * strafeAcceleration;
        }

        if (!forwardPressed && strafeVertical > 0.0f)
        {
            strafeVertical -= Time.deltaTime * strafeDeceleration;
        }
        if (!backwardsPressed && strafeVertical < 0.0f)
        {
            strafeVertical += Time.deltaTime * strafeDeceleration;
        }
        if (!leftPressed && strafeHorizontal < 0.0f)
        {
            strafeHorizontal += Time.deltaTime * strafeDeceleration;
        }
        if (!rightPressed && strafeHorizontal > 0.0f)
        {
            strafeHorizontal -= Time.deltaTime * strafeDeceleration;
        }

    }
    void changeVelocity(bool forwardPressed, bool backwardsPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        //////////////////////IF PRESSING //////////////////////
        if (forwardPressed && speed < currentMaxVelocity)
        {
            speed += Time.deltaTime * acceleration;
        }

        if (leftPressed && speed < currentMaxVelocity)
        {
            speed += Time.deltaTime * acceleration;
        }

        if (rightPressed && speed < currentMaxVelocity)
        {
            speed += Time.deltaTime * acceleration;
        }

        if (backwardsPressed && speed < currentMaxVelocity)
        {
            speed += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && !leftPressed && !rightPressed && !backwardsPressed && speed > 0.0f)
        {
            speed -= Time.deltaTime * deceleration;
        }
        /////////////////////////////
      
    }
    void lockOrResetVelocity(bool forwardPressed, bool backwardsPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        if (forwardPressed && runPressed && speed > currentMaxVelocity)
        {
            speed = currentMaxVelocity;
        }
        else if (forwardPressed && speed > currentMaxVelocity)
        {
            speed -= Time.deltaTime * deceleration;
            // round within offset 
            if (speed > currentMaxVelocity && speed < (currentMaxVelocity + 0.05))
            {
                speed = currentMaxVelocity;
            }
        }
        else if (forwardPressed && speed < currentMaxVelocity && speed > (currentMaxVelocity - 0.05f))
        {
            speed = currentMaxVelocity;
        }

        if (leftPressed && runPressed && speed > currentMaxVelocity)
        {
            speed = currentMaxVelocity;
        }
        else if (leftPressed && speed > currentMaxVelocity)
        {
            speed -= Time.deltaTime * deceleration;
            // round within offset 
            if (speed > currentMaxVelocity && speed < (currentMaxVelocity + 0.05))
            {
                speed = currentMaxVelocity;
            }
        }
        else if (leftPressed && speed < currentMaxVelocity && speed > (currentMaxVelocity - 0.05f))
        {
            speed = currentMaxVelocity;
        }

        if (rightPressed && runPressed && speed > currentMaxVelocity)
        {
            speed = currentMaxVelocity;
        }
        else if (rightPressed && speed > currentMaxVelocity)
        {
            speed -= Time.deltaTime * deceleration;
            // round within offset 
            if (speed > currentMaxVelocity && speed < (currentMaxVelocity + 0.05))
            {
                speed = currentMaxVelocity;
            }
        }
        else if (rightPressed && speed < currentMaxVelocity && speed > (currentMaxVelocity - 0.05f))
        {
            speed = currentMaxVelocity;
        }
        if (backwardsPressed && runPressed && speed > currentMaxVelocity)
        {
            speed = currentMaxVelocity;
        }
        else if (backwardsPressed && speed > currentMaxVelocity)
        {
            speed -= Time.deltaTime * deceleration;
            // round within offset 
            if (speed > currentMaxVelocity && speed < (currentMaxVelocity + 0.05))
            {
                speed = currentMaxVelocity;
            }
        }
        else if (backwardsPressed && speed < currentMaxVelocity && speed > (currentMaxVelocity - 0.05f))
        {
            speed = currentMaxVelocity;
        }
    }
    void lockStrafeVelocity(bool forwardPressed, bool backwardsPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) 
    {
       
        if (forwardPressed && runPressed && strafeVertical > currentMaxVelocity)
        {
            strafeVertical = currentMaxVelocity;
        }
        else if (forwardPressed && strafeVertical > currentMaxVelocity)
        {
            strafeVertical -= Time.deltaTime * deceleration;
            // round within offset 
            if (strafeVertical > currentMaxVelocity && strafeVertical < (currentMaxVelocity + 0.05))
            {
                strafeVertical = currentMaxVelocity;
            }
        }
        else if (forwardPressed && strafeVertical < currentMaxVelocity && strafeVertical > (currentMaxVelocity - 0.05f))
        {
            strafeVertical = currentMaxVelocity;
        }
        ///////////////////////////////////
        if (leftPressed && runPressed && strafeHorizontal < -currentMaxVelocity)
        {
            strafeHorizontal = -currentMaxVelocity;
        }
        else if (leftPressed && strafeHorizontal < -currentMaxVelocity)
        {
            strafeHorizontal += Time.deltaTime * deceleration;
            // round within offset 
            if (strafeHorizontal < -currentMaxVelocity && strafeHorizontal > (-currentMaxVelocity - 0.05))
            {
                strafeHorizontal = -currentMaxVelocity;
            }
        }
        //////////////
        if (rightPressed && runPressed && strafeHorizontal > currentMaxVelocity)
        {
            strafeHorizontal = currentMaxVelocity;
        }
        else if (rightPressed && strafeHorizontal > currentMaxVelocity)
        {
            strafeHorizontal -= Time.deltaTime * deceleration;
            // round within offset 
            if (strafeHorizontal > currentMaxVelocity && strafeHorizontal < (currentMaxVelocity + 0.05))
            {
                strafeHorizontal = currentMaxVelocity;
            }
        }
        ///////////////////////
        if (backwardsPressed && runPressed && strafeVertical < -currentMaxVelocity)
        {
            strafeVertical = -currentMaxVelocity;
        }
        else if (backwardsPressed && strafeVertical < -currentMaxVelocity)
        {
            strafeVertical += Time.deltaTime * deceleration;
            // round within offset 
            if (strafeVertical < -currentMaxVelocity && strafeVertical > (-currentMaxVelocity - 0.05))
            {
                strafeVertical = -currentMaxVelocity;
            }
        }


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

        changeStrafeVelocity(forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        changeVelocity(forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockStrafeVelocity(forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);


        StickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        HandleStandardLocomotionRotation();
        HandleInputData();

        if (IsTargetLocked) HandleTargetLockedLocomotionRotation();
        else HandleStandardLocomotionRotation();
    }
    private void HandleStandardLocomotionRotation()
    {
        Vector3 rotationOffset = mainCamera.transform.TransformDirection(StickDirection);
        rotationOffset.y = 0;
        Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }
    private void HandleTargetLockedLocomotionRotation()
    {
        Vector3 rotationOffset = TargetLock.transform.position - Model.position;
        rotationOffset.y = 0;
        Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }
    private void HandleInputData()
    {
        Anim.SetFloat(speedHash, speed);
        Anim.SetFloat("Horizontal", StickDirection.x);
        Anim.SetFloat("Vertical", StickDirection.z);
        
        Anim.SetFloat(strafeVerticalHash, strafeVertical);
        Anim.SetFloat(strafeHorizontalHash, strafeHorizontal);

        IsWeaponEquipped = Anim.GetBool("IsWeaponEquipped");
        IsTargetLocked = Anim.GetBool("IsTargetLocked");

        if (IsWeaponEquipped && Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetBool("IsTargetLocked", !IsTargetLocked);
            IsTargetLocked = !IsTargetLocked;
        }
        else if (Input.GetKeyDown(KeyCode.F) && !Anim.GetBool("IsAttacking"))
        {
         
            Anim.SetBool("IsWeaponEquipped", !IsWeaponEquipped);
            IsWeaponEquipped = !IsWeaponEquipped;
            if (IsWeaponEquipped == false) 
            {
                Anim.SetBool("IsTargetLocked", false);
                IsTargetLocked = false;
            }
        }
    }
}
