using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool goingRight;
    public int damage = 1;
    private bool hasDamaged = false; // checks if damage has been applied
    // Update is called once per frame
    void Update()
    {
        // if the bullet should move right, move it right, else move it left
        if (goingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
    /// <summary>
    /// When bullet collides with something it destroys itself
    /// </summary>
    /// <returns></returns>
    private void OnCollisionEnter(Collision collision)
    {
        // Checks Collision
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Destroy the object
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Deals with Enemy Damage
    /// </summary>
    /// <param name="other"></param>
        void OnTriggerEnter(Collider other)
        {
            if (!hasDamaged && (other.CompareTag("enemy") || other.CompareTag("bossenemy"))) 
            {
                // Apply damage to the enemy
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                hasDamaged = true;
                }

                // Destroy the bullet
                Destroy(gameObject);
            }
        }
    }
