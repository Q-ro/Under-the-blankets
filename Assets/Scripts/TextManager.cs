using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


public class TextManager : MonoBehaviour {

	public Text textComp;
	public GameObject textBox;
    public GameObject incialTextBox;
    public Text inicialTextComp;
    Animator anim;
	AudioSource guideVoice;
	AudioClip[] guideClips = new AudioClip[5];

	[HideInInspector] public  int currentlyDisplayingText = 0;
	bool displayingText = false;
	bool canClick = true;
    bool isRunning = false;  
    int currentDialog = 0;
	//Stores a dialog to be displayed next
	string[] goatText;
	//All the Level Dialogs
    List<string[]> levelDialogs = new List<string[]>();
    List<string[]> spamDialogs = new List<string[]>();
    List<string[]> goBackDialogs = new List<string[]>();
    List<List<string[]>> failureDialogs = new List<List<string[]>>();
    XmlDocument xmlDoc;

    public static TextManager Instance
    {
        get
        {
            return _intance;
        }
    }

    private static TextManager _intance;

    void Awake()
    {
        if (_intance != null)
        {
            Destroy(_intance);
        }
        _intance = this;

        //Load Xml document
        xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        xmlDoc.Load(Application.dataPath + "/Scripts/XML/dialog.xml");

        //Load Strings
        LoadXmlDoc(5);
        LoadByNameDoc("spamming", spamDialogs);
        LoadByNameDoc("Gb", goBackDialogs);

        List<string[]> list0 = new List<string[]>();
        failureDialogs.Add(list0);

        List<string[]> list1 = new List<string[]>();
        failureDialogs.Add(list1);
        LoadByNameDoc("book", list1);

        List<string[]> list2 = new List<string[]>();
        failureDialogs.Add(list2);
        LoadByNameDoc("bed", list2);

        List<string[]> list3 = new List<string[]>();
        failureDialogs.Add(list3);
        LoadByNameDoc("pills", list3);

        List<string[]> list4 = new List<string[]>();
        failureDialogs.Add(list4);
        LoadByNameDoc("phone", list4);

        List<string[]> list5 = new List<string[]>();
        failureDialogs.Add(list5);
        LoadByNameDoc("cork", list5);

        List<string[]> list6 = new List<string[]>();
        failureDialogs.Add(list6);
        LoadByNameDoc("calendar", list6);

        List<string[]> list7 = new List<string[]>();
        failureDialogs.Add(list7);
        LoadByNameDoc("breathing", list7);

        List<string[]> list8 = new List<string[]>();
        failureDialogs.Add(list8);
        LoadByNameDoc("diary", list8);

        List<string[]> list9 = new List<string[]>();
        failureDialogs.Add(list9);
        LoadByNameDoc("computer", list9);
    }   

    void Start(){
       

        //Set the array to be displayed
        SetCurrentDialog (1);
		anim = textBox.GetComponent<Animator> ();
        //Start text Animation
        //StartCoroutine(AnimateText());

	}

    //Load strings from XML doc specific to the level
    void LoadXmlDoc(int _levelNum)
    {
        if(xmlDoc == null)
        {
            //Load Xml document
            xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
            xmlDoc.Load(Application.dataPath + "/Scripts/XML/dialog.xml");
        }
        
        //Select level
        XmlNodeList levelsList = xmlDoc.GetElementsByTagName("room_" + _levelNum);
        XmlNodeList level = levelsList[0].ChildNodes;

        //For each string in each dialog save it in the level dialogs list
        foreach (XmlNode dialog in level)
        {
            XmlNodeList dialogList = dialog.ChildNodes;
            string[] stringArray = new string[dialogList.Count];
            for (int i = 0; i < stringArray.Length; i++)
            {
                stringArray[i] = dialogList[i].InnerText;
            }
            levelDialogs.Add(stringArray);
        }
    }

    //Load strings from XML doc specific to the level
    void LoadByNameDoc(string name, List<string[]> list)
    {
        if (xmlDoc == null)
        {
            //Load Xml document
            xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
            xmlDoc.Load(Application.dataPath + "/Scripts/XML/dialog.xml");
        }
        //Select level
        XmlNodeList levelsList = xmlDoc.GetElementsByTagName(name);
        XmlNodeList level = levelsList[0].ChildNodes;

        //For each string in each dialog save it in the level dialogs list
        foreach (XmlNode dialog in level)
        {
            XmlNodeList dialogList = dialog.ChildNodes;
            string[] stringArray = new string[dialogList.Count];
            for (int i = 0; i < stringArray.Length; i++)
            {
                stringArray[i] = dialogList[i].InnerText;
            }
            list.Add(stringArray);
        }
    }

    public IEnumerator LoadWithDelay(int _roomNum)
    {
        yield return new WaitForSeconds(1);

        //Load Xml document
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        xmlDoc.Load(Application.dataPath + "/Scripts/XML/dialog.xml");
        //Select level
        XmlNodeList levelsList = xmlDoc.GetElementsByTagName("room_" + _roomNum);
        XmlNodeList level = levelsList[0].ChildNodes;

        //For each string in each dialog save it in the level dialogs list
        foreach (XmlNode dialog in level)
        {
            XmlNodeList dialogList = dialog.ChildNodes;
            string[] stringArray = new string[dialogList.Count];
            for (int i = 0; i < stringArray.Length; i++)
            {
                stringArray[i] = dialogList[i].InnerText;
            }
            levelDialogs.Add(stringArray);
        }
    }

	void SetCurrentDialog(int textId){
        currentDialog = textId;
        goatText = levelDialogs[currentDialog];       
	}


	void Update() {		
		/*if (Input.anyKeyDown) {
			if (textBox.activeSelf) {
				SkipToNextText ();
			}
		}*/
        if (textBox.activeSelf)
        {
            isRunning = true;
        }else
        {
            isRunning = false;
        }
	}

	void SkipToNextText(){
		if (currentlyDisplayingText != goatText.Length && canClick) {
			StopAllCoroutines ();

			if (currentlyDisplayingText > goatText.Length) {
			
			} else if (displayingText == true) {
				textComp.text = goatText [currentlyDisplayingText];
				//currentlyDisplayingText++;
				displayingText = false;
			} else if (displayingText == false) {
				currentlyDisplayingText++;
				StartCoroutine (AnimateText ());
			}
		}
	}

	IEnumerator AnimateText(){
                
        if (currentlyDisplayingText == 0) {
            //yield return new WaitForSeconds (4f);           
            textBox.SetActive (true);
            yield return new WaitForSeconds (1f);
		}
        textComp.text = "";
        for (int j =currentlyDisplayingText; j < goatText.Length; j++) {
			int textLength = goatText [j].Length;
			
			currentlyDisplayingText = j;
			displayingText = true;
			for (int i = 0; i < textLength; i++)
			{ 
				textComp.text += goatText [j][i];
				yield return new WaitForSeconds(.03f);
			}
			displayingText = false;
			yield return new WaitForSeconds(3f);
			if (j != goatText.Length-1) {
				textComp.text = "";
			}
		}
        textComp.text = "";
        anim.SetBool("FadeOut", true);
        yield return new WaitForSeconds(3f);
        anim.SetBool("FadeOut", false);
        GameManager.Instance.canClick = true;
        textBox.SetActive (false);
    }

	public void CallTextBox(int textId){
        GameManager.Instance.canClick = false;
        if (textBox.activeSelf)
        {
            StopAllCoroutines();
        }
        //canClick = true;
		SetCurrentDialog (textId);
		textBox.SetActive (true);
        anim.SetTrigger("FadeIn");
        anim.ResetTrigger("FadeIn");
		currentlyDisplayingText = 0;
		StartCoroutine(AnimateText());
	}

    public void CallInicialTextBox(int textId)
    {
        GameManager.Instance.canClick = false;
        if (textBox.activeSelf)
        {
            StopAllCoroutines();
        }
        //canClick = true;
        SetCurrentDialog(textId);
        incialTextBox.SetActive(true);
        anim.SetTrigger("FadeIn");
        anim.ResetTrigger("FadeIn");
        currentlyDisplayingText = 0;
        StartCoroutine(AnimateInicialText());
    }
    IEnumerator AnimateInicialText()
    {

        if (currentlyDisplayingText == 0)
        {
            //yield return new WaitForSeconds (4f);           
            //textBox.SetActive (true);
            yield return new WaitForSeconds(1f);
        }
        inicialTextComp.text = "";
        for (int j = currentlyDisplayingText; j < goatText.Length; j++)
        {
            int textLength = goatText[j].Length;

            currentlyDisplayingText = j;
            displayingText = true;
            for (int i = 0; i < textLength; i++)
            {
                inicialTextComp.text += goatText[j][i];
                yield return new WaitForSeconds(.03f);
            }
            displayingText = false;
            yield return new WaitForSeconds(2f);
            if (j != goatText.Length - 1)
            {
                inicialTextComp.text = "";
            }
        }
        inicialTextComp.text = "";
        anim.SetBool("FadeOut", true);
        
        yield return new WaitForSeconds(3f);
        anim.SetBool("FadeOut", false);
        GameManager.Instance.canClick = true;
        incialTextBox.SetActive(false);
    }

    public void CallTextBoxSpam()
    {
        GameManager.Instance.canClick = false;
        if (textBox.activeSelf)
        {
            StopAllCoroutines();
        }
        //canClick = true;
        SetCurrentSpamDialog();
        textBox.SetActive(true);
        anim.SetTrigger("FadeIn");
        anim.ResetTrigger("FadeIn");
        currentlyDisplayingText = 0;
        StartCoroutine(AnimateText());
    }

    void SetCurrentSpamDialog()
    {       
        currentDialog = Random.Range(0, spamDialogs.Count - 1);
        goatText = spamDialogs[currentDialog];
    }

    public void CallTextBoxFail(int id, int failRate)
    {
        GameManager.Instance.canClick = false;
        if (textBox.activeSelf)
        {
            StopAllCoroutines();
        }
        //canClick = true;
        SetCurrentFailDialog(id, Random.Range(0,2));
        textBox.SetActive(true);
        anim.SetTrigger("FadeIn");
        anim.ResetTrigger("FadeIn");
        currentlyDisplayingText = 0;
        StartCoroutine(AnimateText());
    }

    void SetCurrentFailDialog(int id, int failRate)
    {
        currentDialog = failRate;
        goatText = failureDialogs[id][currentDialog];
    }

    public bool IsRunning()
    {
         return isRunning;        
    }
}
