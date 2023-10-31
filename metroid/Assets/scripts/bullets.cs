using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Morrison, Brooke 
/// 10/31/23
/// this script makes bullet bill move after it has been spawned
/// </summary>
public class bullets : MonoBehaviour
{
    public float bulletBillSpeed;
    public bool goingRight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnDelay());
    }

    // Update is called once per frame
    void Update()
    {

        //if the laser should move right, move it right, else move it left
        if (goingRight)
        {
            transform.position += Vector3.right * bulletBillSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * bulletBillSpeed * Time.deltaTime;
        }

    }
    /// <summary>
    /// keeps bullet bill in the scene for 3 seconds before he croaks
    /// </summary>
    /// <returns></returns>
    IEnumerator DespawnDelay()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

}
