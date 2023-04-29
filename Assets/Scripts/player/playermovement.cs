using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the player movement script. It applies forces to the player's rigidbody 

// For right now this script is also handling changing the water and lava level- this might change later
public class playermovement : MonoBehaviour
{
    private GameManager gm;

    public Animator anim;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public LayerMask groundMask;

    private bool active = true;

    // new things 
    public CapsuleCollider col;
    private Transform cam;
    private Vector3 moveDir;
    private Vector3 moveRot;

    private bool jumpInput = false;
    private bool jumpHeld = false;

    private Rigidbody rb;

    public float maxSpeed = 6f;
    public float acceleration = 2f;
    public float deccelerationConstant = 0.1f;
    public float deccelerationConstantOnIce = 0.5f;
    public float jumpForce = 15f;

    public float groundCheckDistance = 0.4f;

    public float smallGravityForce = -10f;
    public float gravityForce = -20f;
    private bool smallGravity = false;

    private Vector3 forceLine = new Vector3(0, 0, 0);
    private Vector3 gravityForceLine = new Vector3(0, 0, 0);

    public Vector3 externalMoveSpeed = new Vector3(0, 0, 0);

    public bool turnInDirectionOfMovement = true;

    public PhysicMaterial iceMaterial;
    public bool isOnIce = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDir = new Vector3(0, 0, 0);
        col = GetComponent<CapsuleCollider>();
        gm = GameManager.instance;
        cam = Camera.main.transform;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        // take inputs for later use on the rigidbody

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(GameManager.instance.currentState != GameManager.GameState.Play)
        {
            horizontal = 0f;
            vertical = 0f;
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // convert those inputs into a direction to move the player relative to the camera
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            moveRot = new Vector3(0f, angle, 0f);



            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (moveDir.magnitude > 1f)
            {
                moveDir.Normalize();
            }
        }
        else
        {
            moveDir = new Vector3(0, 0, 0);
            
            
        }

        // check for jump inputs
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
            jumpHeld = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpHeld = false;
        }

        if(GameManager.instance.currentState != GameManager.GameState.Play)
        {
            jumpInput = false;
            jumpHeld = false;
        }

        //Temporary Things- set water/lava levels with numbers
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gm.waterLevel = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gm.waterLevel = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            gm.lavaLevel = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            gm.lavaLevel = 1;
        }
    }

    // method to check if the player is currently grounded via a sphereCast at the players feet
    private bool isGrounded()
    {
        bool returnVal = Physics.SphereCast(transform.position + new Vector3(0, col.radius / 3f), col.radius / 3f - 0.03f, Vector3.down, out var hit, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);
        if( returnVal && hit.collider.material.dynamicFriction == iceMaterial.dynamicFriction && hit.collider.material.dynamicFriction == iceMaterial.dynamicFriction)
        {
            isOnIce = true;
        }
        else
        {
            isOnIce = false;
        }
        return returnVal;
    }

    // funtion that returns the unedited gravity force, which can be large or small (smaller gravity is used when still jumping upwards in order to feel more responsive)
    private Vector3 getGravity()
    {
        if (smallGravity)
        {
            gravityForceLine = new Vector3(0, smallGravityForce, 0);
            return new Vector3(0, smallGravityForce, 0);
        }
        else
        {
            gravityForceLine = new Vector3(0, smallGravityForce, 0);
            return new Vector3(0, gravityForce, 0);
        }
    }

    // function to get the final gravity to be applied to the player, including cancelling any sideways forces the gravity would create from being on a slope
    // (this involves making a raycast at the player's feet to see the angle of the slope they're standing on, and then doing some math to cancel out any sideways forces that would be created by that slope)
    private void addGravityButAccountForSlopes()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, groundCheckDistance + col.radius / 2f - 0.1f, groundMask, QueryTriggerInteraction.Ignore))
        {
            var collider = hit.collider;
            float angle = Vector3.Angle(Vector3.up, hit.normal);
            float currentGravityForce;
            if (smallGravity)
            {
                currentGravityForce = smallGravityForce;
            }
            else
            {
                currentGravityForce = gravityForce;
            }
            float sidewaysForce = currentGravityForce * Mathf.Sin(Mathf.Deg2Rad * angle);
            Vector3 directionOfSidewaysForce = new Vector3(hit.normal.x, 0, hit.normal.z);
            directionOfSidewaysForce.Normalize();
            // get the full force vector pushing the player off the slope and causing them to slide
            Vector3 sideForceVector = directionOfSidewaysForce * sidewaysForce;
            forceLine = sideForceVector;
            gravityForceLine = getGravity();
            rb.AddForce(forceLine + gravityForceLine);
        }
        else
        {
            gravityForceLine = getGravity();
            rb.AddForce(gravityForceLine);
        }
    }

    void FixedUpdate()
    {
        if (active)
        {
            // move/rotate

            // part 1: get to goal velocity
            // this whole thing is a bit hacky
            // ideally increase your velocity in the direction of input by the acceleration parameter
            Vector3 moveVelocity = moveDir * acceleration;
            Vector3 currentHorizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // deccelerate in all directions (adding a bit of friction/responsiveness here)
            if (!isOnIce)//moveDir.magnitude < 0.1f)
            {
                
                if (currentHorizontalVelocity.magnitude > 1)
                {
                    // Cap deceleration in cases where velocity is really high (don't want to stop on a dime when moving really fast)
                    // this will create a linear deceleration when moving fast (>1 m/s)
                    moveVelocity += -currentHorizontalVelocity.normalized * deccelerationConstant;
                }
                else
                {
                    // if you're not moving fast, deccellerate exponentially (nice for responsiveness)
                    moveVelocity += -currentHorizontalVelocity * deccelerationConstant;
                }
            }
            else
            {
                // ice physics version of the above
                if (currentHorizontalVelocity.magnitude > 1)
                {
                    // Cap deceleration in cases where velocity is really high (don't want to stop on a dime when moving really fast)
                    // this will create a linear deceleration when moving fast (>1 m/s)
                    moveVelocity += -currentHorizontalVelocity.normalized * deccelerationConstantOnIce;
                }
                else
                {
                    // if you're not moving fast, deccellerate exponentially (nice for responsiveness)
                    moveVelocity += -currentHorizontalVelocity * deccelerationConstantOnIce;
                }
            }

            if ((moveVelocity + currentHorizontalVelocity).magnitude <= currentHorizontalVelocity.magnitude)
            {
                // if this change would make us slow down (ie we're going the opposite direction), then just do it
                rb.velocity = moveVelocity + currentHorizontalVelocity + new Vector3(0, rb.velocity.y, 0);
            }
            else
            {
                // otherwise, accelerate in the direction of input to a max of the max speed parameter
                // (but if you're already moving faster than max speed, don't slow down because of input)
                rb.velocity = Vector3.ClampMagnitude(moveVelocity + currentHorizontalVelocity, Mathf.Max(maxSpeed, currentHorizontalVelocity.magnitude)) + new Vector3(0, rb.velocity.y, 0);
            }



            if (turnInDirectionOfMovement)
            {
                rb.MoveRotation(Quaternion.Euler(moveRot));
            }
            

            // if on the ground and trying to jump, then jump
            if (jumpInput && isGrounded())
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
                jumpInput = false;
                if (jumpHeld)
                {
                    smallGravity = true;
                }
            }

            // if still jumping and still holding down the button, apply less gravity
            if (!jumpHeld)
            {
                smallGravity = false;
            }
            addGravityButAccountForSlopes();

            // if reached the peak of the jump, apply normal gravity
            if (rb.velocity.y < 0)
            {
                smallGravity = false;
            }
        }
        isGrounded();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lava"){
            gm.RestartScene();
        }
    }

    public void Die()
    {
        active = false;
        gm.RestartScene();
    }

    public void setOnFire()
    {
        //currentSpeed = speedWhenOnFire;
    }

    public void fireExtinguished()
    {
        //currentSpeed = speed;
    }

    public void forceStrongGravity()
    {
        smallGravity = true;
    }

    // draws gizmos when the player object is highlighted in the editor (useful for debugging! Won't do anything in the final release/build.)
    private void OnDrawGizmosSelected()
    {
        // draws a sphere where the isGrounded check (the sphere cast) begins
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,col.radius /3f), col.radius / 3f - 0.03f);
        // draws a sphere where the isGrounded check (the sphere cast) ends
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, groundCheckDistance), col.radius / 3f - 0.03f);
        // draws a vector representing the calculated sideways force that would result from gravity being applied to the player on the current slope
        Gizmos.DrawLine(transform.position, transform.position + forceLine);
        // draws a vector representing the force of gravity on the player
        Gizmos.DrawLine(transform.position, transform.position + gravityForceLine);
        //draws a vector that's the same as the ray cast for slope correction
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckDistance + col.radius /2f - 0.1f));
    }
}
