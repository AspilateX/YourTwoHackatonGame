using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    // Start is called before the first frame update
  
    void Start()
    {
        Destroy(gameObject, 3);
        StartCoroutine(MooveBulbe()); 
    }

    // Update is called once per frame
     void Update()
    {
       
       
    }
    IEnumerator MooveBulbe()
    {
        do
        {
            gameObject.transform.Translate(0, -18, 0);
            yield return new WaitForSeconds(0.1f);
        } while (true);

    }
}
