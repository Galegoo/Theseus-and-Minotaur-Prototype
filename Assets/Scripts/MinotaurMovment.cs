using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinotaurMovment : MonoBehaviour
{
    [SerializeField]
    private LayerMask barrierLayer;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    PlayerMovment playermovment;
    public int playsstorage;
    [SerializeField]
    float intervalBetweenMinotaurMovments;

    public Vector3 minotaurMovementStorage;

    public bool isEnemyMoving;


    // Start is called before the first frame update
    void Awake()
    {
        playermovment = FindObjectOfType<PlayerMovment>();
        playsstorage = playermovment.plays;
        minotaurMovementStorage = transform.position;
        isEnemyMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playsstorage < playermovment.plays && FindObjectOfType<Manager>().gameover == false) //whenever the player makes a play if the game is not over
        {
            minotaurMovementStorage = transform.position; //storage minotaur last position
            playsstorage++;
            StartCoroutine(MinotaurMovementCoroutine()); //calls minotaur movement coroutine
        }
    }

    public void MinotaurMovementMethod() // Minotaur Movement Method
    {
        if (!Physics2D.OverlapCircle(transform.position + new Vector3(-0.4f, 0f, 0f), 0.1f, barrierLayer)) //If there is nothing blocking minotaur left
        {
            if (!Physics2D.OverlapCircle(transform.position + new Vector3(0.4f, 0f, 0f), 0.1f, barrierLayer)) //If there is nothing blocking minotaur right
            {
                if ((transform.position.x - Player.transform.position.x >= 0.2f || transform.position.x - Player.transform.position.x <= -0.2f))// if minotaur and the player don't have the same position on X
                {
                    HorizontalMovement();
                }
                else                                        // there is nothing blocking him but they have the same X, so, vertical movement
                {
                    VerticalMovement();
                }
            }
            else if (transform.position.x - Player.transform.position.x <= 0.2f || transform.position.x - Player.transform.position.x >= -0.2f && transform.position.y != Player.transform.position.y) // if there is something blocking minotaur's right and he has the same x position them the player
            {
                VerticalMovement();
            }
            else if (transform.position.x > Player.transform.position.x) //at this point we already know something is at minotaur's RIGHT and there is nothing tho his LEFT, so now we wanna know if the player is to his left to know if we call vertical movement or try horizontal 
            {
                HorizontalMovement();
            }
            else if (transform.position.x < Player.transform.position.x)
            {
                VerticalMovement();
            }

            else // there is nothing to his left, but there is something to his right so we try to go left
            {
                HorizontalMovement(); // go left
            }
        }
        else if (Physics2D.OverlapCircle(transform.position + new Vector3(0.4f, 0f, 0f), 0.1f, barrierLayer)) //if minotaur is blocked by both sides
        {
            VerticalMovement();
        }
        else if (transform.position.x > Player.transform.position.x) //at this point we already know something is at minotaur's left and right the player is to his right to know if we call vertical movement or try horizontal 
        {
            VerticalMovement();
        }
        else if (transform.position.x < Player.transform.position.x) 
        {
            HorizontalMovement();
        }
        else // horizontal movement is not a possibility, so, we try vertical
        {
            VerticalMovement();
        }
    }

    IEnumerator MinotaurMovementCoroutine() //to make the minotaur wait a bit after the player makes his play and not move at the same time as the player, and to have intervals between minotaur's 2 movements
    {
        yield return new WaitForSeconds(0.2f);
        MinotaurMovementMethod();
        yield return new WaitForSeconds(intervalBetweenMinotaurMovments);
        MinotaurMovementMethod();
        yield return new WaitForSeconds(intervalBetweenMinotaurMovments);
        isEnemyMoving = false;
    }

    private void VerticalMovement()
    {
        if (!Physics2D.OverlapCircle(transform.position + new Vector3(0f, -0.4f, 0f), 0.1f, barrierLayer)) // if there is nothing at minotaur's bottom
            {
            if (transform.position.y > Player.transform.position.y) // moves down if the player is lower
                {
                    transform.Translate(0, -1.1f, 0f);
                    isEnemyMoving = true;
                }
            }

            if (!Physics2D.OverlapCircle(transform.position + new Vector3(0f, 0.4f, 0f), 0.1f, barrierLayer)) // if there is nothing at minotaur's top
        {
                if (transform.position.y < Player.transform.position.y) //  moves up if the player is highier
                {
                    transform.Translate(0f, 1.1f, 0f);
                    isEnemyMoving = true;
                }
            }
    }

    private void HorizontalMovement()
    {
        if (!Physics2D.OverlapCircle(transform.position + new Vector3(-0.4f, 0f, 0f), 0.1f, barrierLayer)) // if there is nothing at minotaur's left
        {
            if (transform.position.x > Player.transform.position.x) //moves left if the player is to the left
            {
                transform.Translate(-1.1f, 0f, 0f);
                isEnemyMoving = true;
            }
        }

        if (!Physics2D.OverlapCircle(transform.position + new Vector3(0.4f, 0f, 0f), 0.1f, barrierLayer)) // if there is nothing at minotaur's right
        {
            if (transform.position.x < Player.transform.position.x) // moves right if the player is to the right
            {
                transform.Translate(1.1f, 0f, 0f);
                isEnemyMoving = true;
            }
        }
    }
}


