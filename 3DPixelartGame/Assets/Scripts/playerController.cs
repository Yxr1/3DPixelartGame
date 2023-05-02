using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;    
    public float gravity = -9.81f;
    private CharacterController controller;   // The character controller component of the player
    private Vector3 moveDirection;   

    private bool isDodging = false; 
    public float dodgeSpeedResetTime = 5f;
    public float dodgeCooldown = 0.7f;     

    public float maxHealth = 100;
    public float currentHealth = 80;
    public float EXP = 0;
    public float playerLevel = 1;
    public Text healthText;
    public Text EXPText;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Healthcheck
        if(currentHealth <= 0){
            horizontalInput = 0;
            verticalInput = 0;
            Debug.LogWarning("NO HEALTH");
        }

        // Check if the player is grounded
        if (controller.isGrounded)
        {
            // Calculate the movement direction based on the horizontal input
            moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
            moveDirection *= moveSpeed;

            // Jump if the space bar is pressed
            if (Input.GetKeyDown(KeyCode.Space) && !isDodging)
            {
                moveSpeed = 3 * moveSpeed;
                isDodging = true;
                Invoke("DodgeResetSpeed", dodgeSpeedResetTime);
                Invoke("DodgeCooldownReset", dodgeCooldown);
            }
            
        }

        //testing for EXP
        if (Input.GetKeyDown(KeyCode.T)){
                decreaseHealth(50);
        }

        // Apply gravity to the movement direction
        moveDirection.y += gravity * Time.deltaTime;

        // Move the player using the character controller component
        controller.Move(moveDirection * Time.deltaTime);
        UpdateUI();
    }

    private void UpdateUI(){
        healthText.text = "Health: " + currentHealth.ToString() + " / " + maxHealth.ToString();
        EXPText.text = "EXP: " + EXP.ToString();
    }

    private void DodgeResetSpeed(){
        moveSpeed = moveSpeed / 3;
        Debug.Log("RESET DODGE SPEED");
    }

    private void DodgeCooldownReset(){
        isDodging = false;
        Debug.Log("RESET DODGE COOLDOWN");
    }

    private void decreaseHealth(float amount){
        currentHealth -= amount;
    }
}