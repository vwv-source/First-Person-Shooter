using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float MouseSenesitivty = 100f;
    public float XRotation = 0f;
    public float speed = 12f/2;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpDistance = 3f;

    Vector3 velocity;

    bool isGrounded;
    bool isCrouched = false;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] GameObject PlayerBody;
    [SerializeField] CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.C)){
            if(isCrouched){
                isCrouched = false;
                PlayerBody.transform.localScale = new Vector3(1,1,1);
            }else{
                isCrouched = true;
                speed = 5f;
                PlayerBody.transform.localScale = new Vector3(PlayerBody.transform.localScale.x - 0.4f,PlayerBody.transform.localScale.y - 0.4f,PlayerBody.transform.localScale.z - 0.4f);
            }
        }

        //---------------------------------------------------

        float MouseX = Input.GetAxis("Mouse X") * MouseSenesitivty * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSenesitivty * Time.deltaTime;

        XRotation -= MouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation,0,0);
        PlayerBody.transform.Rotate(Vector3.up, MouseX);

        //---------------------------------------------------

        if(Input.GetKey(KeyCode.LeftShift) && !isCrouched){
            speed = 20f/2;
        }
        else{
            speed = 12f/2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //---------------------------------------------------

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -4f;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            velocity.y = Mathf.Sqrt(jumpDistance * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
