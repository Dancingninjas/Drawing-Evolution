using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trail : MonoBehaviour {
    //public Image ColorImage;
    //public TrailRenderer currentTR;
    //Color color = Color.blue;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
       
        //if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButtonDown(0))
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("UPPP");
            gameObject.GetComponent<TrailRenderer>().emitting = false;
            this.enabled = false;
        }
        else
        {
            gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z + 10);
        }
    }
    //public IEnumerator enableEmitting()
   // {
    //    yield return new WaitForSeconds(1f);
    //    gameObject.GetComponent<TrailRenderer>().emitting = true;
    //}

}
