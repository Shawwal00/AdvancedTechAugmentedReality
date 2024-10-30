using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ReEnableScript : MonoBehaviour
{
    /*
        The Enhanced input won't work unless the script is turned on and off so doing that in this script
    */


    private ShowingFamousPerson thisScript;

    // Start is called before the first frame update
    void Start()
    {
        thisScript = GetComponent<ShowingFamousPerson>();
        thisScript.enabled = false;

        StartCoroutine(EnableCoroutine());
    }

    IEnumerator EnableCoroutine()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        thisScript.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
