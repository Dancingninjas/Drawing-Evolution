using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveLeftUntilOffScreen : MonoBehaviour {
    RectTransform Cloud;
	// Use this for initialization
	void Start () {
        Cloud = gameObject.GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {
        //Cloud.Translate(Vector3.left * Time.deltaTime * 50);
        Cloud.anchoredPosition = Vector3.MoveTowards(Cloud.anchoredPosition, new Vector3(-900, Cloud.anchoredPosition.y, 0), 35 * Time.deltaTime);
        if (Cloud.anchoredPosition.x < -800)
        {
            Cloud.anchoredPosition = new Vector3(800, Cloud.anchoredPosition.y, 0);
        }

    }
}
