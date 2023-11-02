using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Morrison, Brooke & Melendrez, Servando
/// 10/31/23
/// This script controls the full movment of the player. Also controls collisions
/// </summary>
public class playerController : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rigidbodyRef;
    public float jumpForce = 10f;
    public float deathYLevel = -3;
    public int lives = 3;
    public float totalHealth = 99f;
    private Vector3 startPos;
    private bool jetpackCollected = false;
    private bool doubleJump = false;
    private bool canTakeDamage = true;
    public bool isGroundedtoFloor;

    // Start is called before the first frame update
    void Start()
    {
        //gets the rigidbody component off of this object and stores a reference to it
        rigidbodyRef = GetComponent<Rigidbody>();

        //set the statrting position
        startPos = transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        
            //side to side player movement
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }

           /* if (transform.position.y <= deathYLevel)
            {
                Respawn();
            }*/

            //CheckForDamage();
        }
    

  /*  private void Respawn()
    {
        if (canTakeDamage == true)
        {


            //teleport the player to the starting position
            //cause the player to lose a life

            lives--;
            transform.position = startPos;

            StartCoroutine(SetInvincibility());

            if (lives == 0)
            {
                //add code to end the game by loading the game over scene
                SceneManager.LoadScene(2);  //change scene number later
                Debug.Log("Game Ends");
            }
        }

    }*/

    private void Die()
    {
        if(canTakeDamage == true)
        {

              StartCoroutine(SetInvincibility());

            if (totalHealth == 0)
            {
                //add code to end the game by loading the game over scene
                SceneManager.LoadScene(2);  //change scene number later
                Debug.Log("Game Ends");
            }
        }
    }
    /// <summary>
    /// check to see if a thwomp hits the player from the top
    /// </summary>
  /*  private void CheckForDamage()
    {
        RaycastHit hit;
        //raycast upwards and check the raycast will return true if it hits an object
        //raycast(startPost, direction, output hit, distance for ray);
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1))
        {
            //check to see if the object hitting the player is a thwomp
            if (hit.collider.tag == "thwomp")
            {
                Respawn();
            }
        }
    }*/
   /* private bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f);
        return true;

    }*/
    /// <summary>
    /// makes sure the player is touching the ground before they are allowed to jump
    /// </summary>
    private void HandleJump()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out Hit, 1.5f))
        {
            isGroundedtoFloor = true;
            Debug.Log("single jumping");
        }
        else
        {
            isGroundedtoFloor = false;
            Debug.Log("not grounded");
        }

        if((Input.GetKeyDown(KeyCode.Space) && isGroundedtoFloor == true) && !jetpackCollected)
        {
            rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isGroundedtoFloor || doubleJump) && jetpackCollected)
            {
                rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jetpack jump " + doubleJump);
            doubleJump = !doubleJump;
            
            }
    }
    /// <summary>
    /// makes the coins add to the total coin value and causes them to dissapear afterwards
    /// </summary>
    /// <param name="other">The object that is being collided with</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "portal")
        {
           // move the player to the teleport point's position stored on the portal object
           transform.position = other.gameObject.GetComponent<portal>().teleportPoint.transform.position;
            startPos = transform.position;
        }
        if(other.gameObject.tag == "jetpack")
        {
            jetpackCollected = true;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "enemy")
        {
            totalHealth -= 15f;
        
        }
        if(other.gameObject.tag == "bossenemy")
        {
            totalHealth -= 35f;
        }
    }
    
    IEnumerator SetInvincibility()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(5f);
        canTakeDamage = true;
    }

}

