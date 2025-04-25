using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField]
    private LayerMask barrierLayer;
    private Manager manager_ref;
    private MinotaurMovment minotaur_ref;

    public int plays;

    public Vector3 playerMovementStorage;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementStorage = transform.position; //store the initial player's position
        manager_ref =  FindObjectOfType<Manager>();
        minotaur_ref = FindObjectOfType<MinotaurMovment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager_ref.gameover && minotaur_ref.isEnemyMoving == false)// if game is not over and minotaur is not moving the player can move
            Movement();
    }

    private void Movement() // Method reponsible for the Player movment;
    {
        if (!Physics2D.OverlapCircle(transform.position + new Vector3(0f, 0.4f, 0f), 0.2f, barrierLayer)) //makes not possible to move when close to a wall or screen limits
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerMovementStorage = transform.position; //store the last player's position
                transform.Translate(0f, 1.1f, 0f);
                plays++;
                minotaur_ref.isEnemyMoving = true;
            }            
        }

        if (!Physics2D.OverlapCircle(transform.position + new Vector3(0f, -0.4f, 0f), 0.2f, barrierLayer))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerMovementStorage = transform.position; //store the last player's position
                transform.Translate(0f, -1.1f, 0f);
                plays++;
                minotaur_ref.isEnemyMoving = true;
            }             
        }

        if (!Physics2D.OverlapCircle(transform.position + new Vector3(-0.4f, 0f, 0f), 0.2f, barrierLayer))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerMovementStorage = transform.position; //store the last player's position
                transform.Translate(-1.1f, 0f, 0f);
                plays++;
                minotaur_ref.isEnemyMoving = true;
            }            
        }         

        if (!Physics2D.OverlapCircle(transform.position + new Vector3(0.4f, 0f, 0f), 0.2f, barrierLayer))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerMovementStorage = transform.position; //store the last player's position
                transform.Translate(1.1f, 0f, 0f);
                plays++;
                minotaur_ref.isEnemyMoving = true;
            }          
        }      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy") //If the player collides with the minotaur, calls game over method
        {
            manager_ref.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Exit") //if the player collides to the exit, he wins
        {
            manager_ref.Win();
        }
    }
}
