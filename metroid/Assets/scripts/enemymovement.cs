using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Morrison, Brooke and Melendrez, Servando
/// 10/31/23
/// This script controls the enemy's movement from side to side
/// </summary>

public class enemymovement : MonoBehaviour
{
    public float travelDistanceRight = 0f;
    public float travelDistanceLeft = 0f;
    public float speed;

    private float startingX;
    //private float startingY;
    private bool movingRight = true;



    // Start is called before the first frame update
    void Start()
    {
        //when the scene starts, store the initial x value of this object
        startingX = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            //if the object is not farther than the start position plus right travel distance, it can move right
            if (transform.position.x <= startingX + travelDistanceRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            //if the object is not farther than the start position + left travel distance, it can move 

            if (transform.position.x >= startingX + travelDistanceLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            //if the object goes too far left, tell it to move right
            else
            {
                movingRight = true;
            }
        }

    }
}
