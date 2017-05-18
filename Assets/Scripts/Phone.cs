using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour {

    public GameObject phone;
    public GameObject arrow;
    public Text clock;    

    bool isShowned;

	// Use this for initialization
	void Start () {
        isShowned = false;
	}
	
	// Update is called once per frame
	void Update () {
        //string time[] = System.DateTime.Now.ToString().Split(' ', '\t');
        //string date = time.Split(" ");
        clock.text = System.DateTime.Now.ToString();
    }

    public void ShowPhone()
    {
        if (!isShowned)
        {
            //phone.SetActive(true);
            phone.GetComponent<Animator>().SetTrigger("PopUp");
            GameManager.Instance.phoneOut = true;
        }
        else
        {
            phone.GetComponent<Animator>().SetTrigger("PopDown");
            StartCoroutine(PhoneDown());            
        }
    }

    private void OnMouseOver()
    {
        if (GameManager.Instance.currentActionIndex == 5 && GameManager.Instance.canClick)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }

    }

    private void OnMouseExit()
    {        
         gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    IEnumerator PhoneDown()
    {
        yield return new WaitForSeconds(1.5f);
        //phone.SetActive(false);
    }

    public void RemovePhone()
    {
        phone.GetComponent<Animator>().SetTrigger("PopDown");
        GameManager.Instance.phoneOut = false;
    }
}
