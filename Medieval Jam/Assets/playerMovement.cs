using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animations;

    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 5.0f;

    bool isGrounded;
    private FactionManager factionManager;

    private bool mouseDown = false;
    // Start is called before the first frame update
    void Start()
    {
        //FactionManager factionManager = FactionManager.GetInstance(this.gameObject);              //I commented this out bc it was set to null in the prefab
        //factionManager.RegisterPlayerObject(this.gameObject);                                     //just uncomment when you implement it I guess
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;               
        }

        float x = Input.GetAxis("Horizontal");              //input
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;     

        controller.Move(move*speed*Time.deltaTime);         //movement

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight *-2f*gravity);       //jump
        }

        animations.SetFloat("speed", Mathf.Sqrt((float)(x * x) + (float)(z * z)));      //code to check update the animator on the speed: in the animator, if speed > 0.1, 
                                                                                        //it'll start playing the walking animation

        if (Input.GetMouseButtonDown(0)&&!mouseDown)    
        {
            mouseDown = true;
            animations.SetTrigger("Attack");        //code to trigger an attack animation
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        velocity.y += gravity;
        controller.Move(velocity * Time.deltaTime);
    }
}
