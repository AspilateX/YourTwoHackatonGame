using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPanelClick : MonoBehaviour
{
    public Chemestry Script;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        
        Script.ChangeTextById(text.text);

    }
}
