using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateTower : MonoBehaviour
{
    InputSystem inputSystem;
    TopPanelAnimator topPanelAnimator;

    public GameObject tower;
    public GameObject topPanel;
    public Renderer[] spawnPoints;

    public Button tower1Button;
    public Button removeButton;

    float scaleY;
    Vector3 spawnPointPosition;
    Quaternion rot;
    GameObject GOtoRemove;
    GameObject spawnPointGO;
    void Awake()
    {
        inputSystem = ConfigurationScript.getProjectInputSystem();
        topPanelAnimator = GameObject.Find("TopPanel").GetComponent<TopPanelAnimator>();    //reference to Animator script

        arrayOfRendererSpawnPoints();   //make an array of SpawnPoints
    }
    void Update()
    {
        Spawning();
    }
    void Spawning()
    {
        if (inputSystem.cursorDown())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(inputSystem.getCursorCoordinates());
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag == "TowerSpawnPosition" && hit.transform.childCount==0)    //if no tower on spawnPoint
                {
                    tower1Button.interactable = true;
                    removeButton.interactable = false;
                    colorReset(spawnPoints);    //reset all to main color
                    choosenColor(hit.transform.gameObject); //change choosen one color
                    topPanelAnimator.TopPanelDown();    //start Top Panel anim
                    scaleY = (tower.transform.localScale.y) / 2;    //imo not final ver. 
                    spawnPointGO = hit.transform.gameObject;    //chosen one gameObject
                    spawnPointPosition = new Vector3(hit.transform.position.x, hit.transform.position.y + scaleY, hit.transform.position.z);
                    rot = hit.transform.rotation;
                }
                else if(hit.transform.tag == "Tower")
                {
                    tower1Button.interactable = false;
                    removeButton.interactable = true;
                    colorReset(spawnPoints);    //reset all to main color
                    choosenColor(hit.transform.parent.gameObject); //change parent(spawnPoint) color
                    topPanelAnimator.TopPanelDown();    //start Top Panel anim
                    scaleY = (tower.transform.localScale.y) / 2;    //imo not final ver. 
                    GOtoRemove = hit.transform.gameObject;  //gameObject to remove(tower)
                    spawnPointPosition = new Vector3(hit.transform.parent.position.x, hit.transform.parent.position.y + scaleY, hit.transform.parent.position.z);   
                    //.parent because want spawnPoint pos. not tower pos.
                    rot = hit.transform.parent.rotation;
                }
            }

        }
    }
    public void tower1Click()
    {
        Instantiate(tower, spawnPointPosition, rot, spawnPointGO.transform);
        tower1Button.interactable = false;
        removeButton.interactable = true;
        GOtoRemove = spawnPointGO.transform.GetChild(0).gameObject; //by spawnPoint gameObject get tower gameObject(which we want remove)
    }
    public void removeClick()
    {
        Destroy(GOtoRemove);    //remove tower
        removeButton.interactable = false;
        tower1Button.interactable = true;
    }
    void arrayOfRendererSpawnPoints()   //create array of spawnPoint gameObjects
    {
        GameObject[] spawnPointsObjects = GameObject.FindGameObjectsWithTag("TowerSpawnPosition");
        spawnPoints = new Renderer[spawnPointsObjects.Length];
        for (int i = 0; i < spawnPointsObjects.Length; ++i)
            spawnPoints[i] = spawnPointsObjects[i].GetComponent<Renderer>();
    }
    void choosenColor(GameObject hit)   //display choosen one spawnPoint
    {
        Renderer col = hit.transform.GetComponent<Renderer>();
        col.material.color = Color.black;
    }
    public void colorReset(Renderer[] arr)  //all of spawnPoints in main state
    {
        for(int i=0;i<arr.Length;i++)
        {
            arr[i].material.color = Color.white;
        }
    }
}

