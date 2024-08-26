using System.Collections;
using System.Collections.Generic;
using UnityEngine; // essential 

public class Player : MonoBehaviour
{   
    [SerializeField] private Transform groundCheckTransform; 
    [SerializeField] LayerMask playerMask;

    // methods are private by default 
    bool jumpKeyWasPressed; 
    float horizontalInput; 
    Rigidbody rigidbodyComponent;
    int superJumpRemaining; 
    

    // Start is called before the first frame update
    void Start() // void means this method doesnt return anything
    {
        rigidbodyComponent = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {   
        // check if the space key is pressed down 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true; 
        }
        horizontalInput = Input.GetAxis("Horizontal"); 
    }
    
    // FixedUpdate is called once every physic update 
    void FixedUpdate()
    {   
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0); 
        // if it's 0, it's not colliding with anything 
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return; 
        }
        // chech if the space key is pressed down 
        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f; 
            if (superJumpRemaining > 0)
            {
                jumpPower*=2; 
                superJumpRemaining--; 
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false; 
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(other.gameObject); 
            superJumpRemaining++; 
        }
    }
}
