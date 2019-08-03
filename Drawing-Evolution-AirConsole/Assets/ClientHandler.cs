using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientHandler : MonoBehaviour {
    int ClientsConnected;
    public GameObject trailPrefab;
    public Color trailColor;
    public Image ColorImage;
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        ColorImage.color = Color.black;
        trailColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject currentTrail = Instantiate(trailPrefab, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z + 10), Quaternion.identity);
            currentTrail.GetComponent<TrailRenderer>().startColor = trailColor;
            currentTrail.GetComponent<TrailRenderer>().endColor = trailColor;
        }
    }
    public void StartGame()
    {
        Debug.Log("Start");
    }




    public void newColor()
    {
        //int random = Random.Range(0, 3);
        if (trailColor == Color.black)
        {
            trailColor = Color.green;
        }
        else if (trailColor == Color.green)
        {
            trailColor = Color.red;
        }
        else if (trailColor == Color.red)
        {
            trailColor = Color.yellow;
        }
        else if (trailColor == Color.yellow)
        {
            trailColor = Color.magenta;
        }
        else if(trailColor == Color.yellow)
        {
            trailColor = Color.blue;
        }
        else
        {
            trailColor = Color.black;
        }
        ColorImage.color = trailColor;
    }
    public void ResetCanvas()
    {
        GameObject[] allTrails = GameObject.FindGameObjectsWithTag("Trail");
        foreach (GameObject i in allTrails)
        {
            Destroy(i);
        }
    }
}
