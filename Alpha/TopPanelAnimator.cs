using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPanelAnimator : MonoBehaviour {

    Animator anim;
    InstantiateTower instTower;
    void Awake () {
        anim = GetComponent<Animator>();
        instTower = GameObject.FindGameObjectWithTag("GameController").GetComponent<InstantiateTower>();    //reference script
	}
	
    public void TopPanelDown()
    {
       anim.SetBool("TopPanelOn", true);
    }
    public void TopPanelUp()
    {
        anim.SetBool("TopPanelOn", false);
        instTower.colorReset(instTower.spawnPoints);    //set all spawnPoints in main state
    }
}
