using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusScript : MonoBehaviour
{
    public int health = 100;
    public HealthBarScript healthbar;
    

    private GameObject parentGameObject;

    public void DecreaseHealthByTheNumber(int healthToBeDecreased)
    {
        health = health - healthToBeDecreased;
                     

    }


    // Start is called before the first frame update
    void Start()
    {
        // simply for testing    
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
