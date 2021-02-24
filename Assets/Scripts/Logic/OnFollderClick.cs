using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnFollderClick : MonoBehaviour
{
    public Chemestry Script;
    public Text text;
    public int id;
 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void SetText(string newtext)
    {
        text.text = newtext;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        Script.clickid = id;
        Script.ShowPanel();

    }
}
