using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogScript : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject textBox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
   // public GameObject objectDestroy;

   void Start()
    {
        
    }

     void Update()
    {
     if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            textBox.SetActive(true);
            StartCoroutine(Type());
        }
    }



    public IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length-1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            textBox.SetActive(false);
        }
    }

}