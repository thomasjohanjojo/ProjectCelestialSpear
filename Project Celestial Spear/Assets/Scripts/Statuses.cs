using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Statuses : MonoBehaviour
{
    /// <summary>
    /// This script manages the status values and effects of the gameobject to which this is attached
    /// </summary>


    // The statuses and their respective functions, in that order for each status-functions group
    public int health = 100;
    public HealthBarScript healthbar;
    public PlayerAttack playerAttackScriptReference;
    public bool playerCanBeDamaged;

    public bool playerHasBeenDamaged;
    public bool playerHasBeenStunned;

    private GameObject parentGameObject;

    public void DecreaseHealthByTheNumber(int healthToBeDecreased, BoxCollider2D theBoxColliderWhichDealsTheAttack)
    {
        

        if (playerCanBeDamaged == true)
        {
            health = health - healthToBeDecreased;


            if (this.gameObject.tag == "Player")
            {
                playerAttackScriptReference.HitCounterInt = 0;
            }

            playerHasBeenDamaged = true;
            playerHasBeenStunned = true;
        }

        theBoxColliderWhichDealsTheAttack.enabled = false;

    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        // simply for testing    
        playerCanBeDamaged = true;
        playerHasBeenDamaged = false;
        playerHasBeenStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthbar)
        {
            healthbar.Sethealth(health);
        }
       /* if (health <= 0.0f)
        {
            if (this.transform.parent)
            {
                parentGameObject = this.transform.parent.gameObject;

                if (parentGameObject.tag == "Platform")
                {
                    

                    Destroy(this.gameObject);

                    if (this.gameObject.tag == "Player")
                    {
                        SceneManager.LoadScene(0);
                    }

                }

                else
                {
                    Destroy(this.gameObject);
                    Destroy(parentGameObject);

                    if (this.gameObject.tag == "Player")
                    {
                        SceneManager.LoadScene(0);
                    }
                }
            }

            else
            {
                

                Destroy(this.gameObject);


                if (this.gameObject.tag == "Player")
                {
                    SceneManager.LoadScene(0);
                }

            }
        }*/
    }
}
