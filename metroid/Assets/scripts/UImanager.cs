using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Morrison, Brooke
/// 10/23/23
/// This script displays the coins and lives on the top right hand corner of the game view
/// </summary>
public class UImanager : MonoBehaviour
{
    public playerController playerController;
    public TMP_Text healthDisplay;
    //public TMP_Text livesDisplay;


    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = "Health: " + playerController.totalHealth;
       // livesDisplay.text = "Lives: " + playerController.lives;
    }
}
