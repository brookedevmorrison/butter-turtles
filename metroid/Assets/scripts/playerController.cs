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
    private bool canTakeDamage = false;
    private Renderer[] renderers;
    public bool isGroundedtoFloor;
    private bool isFacingLeft = false;
    //Shooting Variables
    public GameObject Bullet;
    public bool shootRight = true;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        //gets the rigidbody component off of this object and stores a reference to it
        rigidbodyRef = GetComponent<Rigidbody>();

        //set the statrting position
        startPos = transform.position;
        // Get the Renderer components of the player and its children
        renderers = GetComponentsInChildren<Renderer>();
        // Ensure that all renderers are initially visible
        SetRenderersVisibility(true);
        // Set canTakeDamage to true after initializing renderers
        canTakeDamage = true;
    }



    // Update is called once per frame
    void Update()
    {

        //side to side player movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (!isFacingLeft) // Check if the player is not already facing left
            {
                isFacingLeft = true;
                RotatePlayerModel(-90f); // Rotate the player model 180 degrees on the X-axis
               
            }
            shootRight = false;
            // Handle player movement to the left
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (isFacingLeft) // Check if the player is not already facing right
            {
                isFacingLeft = false;
                RotatePlayerModel(90f); // Rotate the player model back to its original rotation
               
            }
            shootRight = true;
            // Handle player movement to the right
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }
        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            StartCoroutine(ShootWithDelay());
        }
        Die();
           /* if (transform.position.y <= deathYLevel)
            {
                Respawn();
            }*/

            //CheckForDamage();
        }
    /// <summary>
    /// Shoots bullet
    /// </summary>
        private void ShootBullet()
    {
        GameObject bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, 0));
        bulletInstance.GetComponent<Bullet>().goingRight = shootRight;

    }
    private IEnumerator ShootWithDelay()
    {
        canShoot = false; // Disable shooting
        ShootBullet();

        yield return new WaitForSeconds(0.5f); // 0.5-second delay

        canShoot = true; // Enable shooting after the delay
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
            if (totalHealth <= 0f)
            {
                //add code to end the game by loading the game over scene
                SceneManager.LoadScene(1);  //change scene number later
                Debug.Log("Game Ends");
            }
    }

    private void Blink()
    {
        if (canTakeDamage) 
        {
                StartCoroutine(SetInvincibility());
        }
    }
    // Rotate the player model on the X-axis
    private void RotatePlayerModel(float angle)
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y = angle;
        transform.rotation = Quaternion.Euler(currentRotation);
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
        if(other.gameObject.tag == "enemy" && canTakeDamage)
        {
            totalHealth -= 15f;
            Blink();
        
        }
        if(other.gameObject.tag == "bossenemy" && canTakeDamage)
        {
            totalHealth -= 35f;
            Blink();
        }
    }

    IEnumerator SetInvincibility()
    {
        canTakeDamage = false;

        // Blink duration and blink speed settings
        float blinkDuration = 5f;
        float blinkSpeed = 0.2f;

        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            SetRenderersVisibility(!AreRenderersVisible()); // Toggle visibility of all renderers
            yield return new WaitForSeconds(blinkSpeed);
            elapsedTime += blinkSpeed;
        }

        SetRenderersVisibility(true); // Ensure all renderers are visible at the end
        canTakeDamage = true;
    }

    /// <summary>
    /// Sets the visibility of all renderers
    /// </summary>
    /// <param name="visible"></param>
    private void SetRenderersVisibility(bool visible)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = visible;
        }
    }

    /// <summary>
    /// Checks if any renderer is currently visible
    /// </summary>
    /// <returns></returns>
    private bool AreRenderersVisible()
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer.enabled)
            {
                return true;
            }
        }
        return false;
    }
}

