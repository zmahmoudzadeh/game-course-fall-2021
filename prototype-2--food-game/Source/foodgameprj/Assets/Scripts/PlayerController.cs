﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Range(0f, 1f)] public float moveAmount;

    public int playerScore;
    public int playerHeartsCount;
    public Text scoreText;
    public Text heartText;

    private void Start()
    {
        playerScore = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveAmount, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveAmount, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            // access the food object config
            FoodItemConfig conf = collision.gameObject.GetComponent<FoodInstanceController>().config;

            // increase the player's score
            playerScore += conf.score;

            Debug.Log("SCORE: " + playerScore);
            scoreText.text = "SCORE: " + playerScore.ToString();

            // destroy the food object
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Combo"))
        {
            // polymorphism!
            // for example, the object of type "TimeFreezerComboController" which is the child of "ComboInstanceController", is put inside the "comboController" object below.
            ComboInstanceController comboController =  collision.gameObject.GetComponent<ComboInstanceController>();

            // the CONTENT of OnConsume method inside "TimeFreezerComboController" is available inside the "comboController"
            comboController.OnConsume();

            Debug.Log("COMBO!!! " + comboController.config.comboName);

            // destroy the combo object
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Heart"))
        {
            // access the heart object config
            HeartItemConfig conf = collision.gameObject.GetComponent<HeartInstanceController>().config;

            // increase the player's hearts
            playerHeartsCount += conf.value;

            Debug.Log("HEART: " + playerHeartsCount);
            heartText.text = "HEART: " + playerHeartsCount.ToString();

            // destroy the heart object
            Destroy(collision.gameObject);
        }
    }
}
