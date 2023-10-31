using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Morrison, Brooke
/// 10/31/23
/// This script controls the full movment of the player. Also controls collisions
/// </summary>
public class playerController : MonoBehaviour
{
    public float speed = 10f;
   // public int totalCoins = 0;
    private Rigidbody rigidbodyRef;
    public float jumpForce = 10f;
    public float deathYLevel = -3;
    public int lives = 3;
    public float totalHealth = 99f;
    private Vector3 startPos;
    private bool jetpackCollected = false;
    private bool doubleJump = false;
   // public bool stunned = false;

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

            //jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }

            if (transform.position.y <= deathYLevel)
            {
                Respawn();
            }

            CheckForDamage();
        }
    

    private void Respawn()
    {
        //teleport the player to the starting position
        //cause the player to lose a life

        lives--;
        transform.position = startPos;

        if (lives == 0)
        {
            //add code to end the game by loading the game over scene
            SceneManager.LoadScene(2);  //change scene number later
            Debug.Log("Game Ends");
        }

    }
    /// <summary>
    /// check to see if a thwomp hits the player from the top
    /// </summary>
    private void CheckForDamage()
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
    }
    private bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f);
        return true;
    }
    /// <summary>
    /// makes sure the player is touching the ground before they are allowed to jump
    /// </summary>
    private void HandleJump()
    {
        

        if (isGrounded() && !Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Player is touching to ground so jump");
            rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (doubleJump == false && jetpackCollected == true)
            {
                doubleJump = true;
                Debug.Log("Player has jetpack so double jump is enabled.");
                rigidbodyRef.AddForce(Vector3.up * (jumpForce + 2), ForceMode.Impulse);
            }
        }
        else
        {
            Debug.Log("Player is not touching to ground so they cannot jump");
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
        if (other.gameObject.tag == "bullet")
        {
            Respawn();
        }
    }
    


}

