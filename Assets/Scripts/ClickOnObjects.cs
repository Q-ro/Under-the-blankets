using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnObjects : MonoBehaviour {

    public AudioSource mouseClick;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.Instance.phoneOut)
        {            
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.canClick)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

                if (hitCollider && hitCollider.gameObject.tag == "HasEvent")
                {
                    mouseClick.Play();
                    hitCollider.gameObject.GetComponent<CallEvent>().CallEventOnClick();
                }
                else if (hitCollider && hitCollider.gameObject.tag == "Phone")
                {
                    mouseClick.Play();
                    hitCollider.gameObject.GetComponent<Phone>().ShowPhone();
                }
            }
        }
    }
}
