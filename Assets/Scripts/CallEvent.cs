using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEvent : MonoBehaviour {
    
    public int id;
    public int anxiety;
    public Transform wayPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        if(id == GameManager.Instance.sequence[GameManager.Instance.currentActionIndex] && id !=0 && GameManager.Instance.canClick)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
       
    }

    private void OnMouseExit()
    {
        if(id != 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        
    }

    public void CallEventOnClick()
    {        
        GameManager.Instance.CallEvent(id, anxiety, wayPoint);
    }

    public void PlayPhoneRing()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }        
    }
}
