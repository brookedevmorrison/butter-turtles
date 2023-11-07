using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Morrison Brooke and Melendrez Servando
/// 11/7/23
/// This script makes the hard enemy follow the player
/// </summary>
public class hardenemyfollow : MonoBehaviour
{

    public GameObject waypoint;

    public Vector3 waypointPosition;

    public float speed = 2f;



    // Start is called before the first frame update
    void Start()
    {
        waypoint = GameObject.Find("waypoint");
    }

    // Update is called once per frame
    void Update()
    {
        waypointPosition = new Vector3(waypoint.transform.position.x, transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, waypointPosition, speed * Time.deltaTime);
    }
}
