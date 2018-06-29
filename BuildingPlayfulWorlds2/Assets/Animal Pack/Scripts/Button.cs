using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject sheep;
	public GameObject hare;
	public GameObject buffalo;
    public GameObject stag;
	public GameObject alligator;
	public GameObject marlin;
    public GameObject panther;
	public GameObject elephant;
	public GameObject bear;
	public GameObject horse;
	public GameObject saddle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Sheep(){
		Clear();
		sheep.SetActive(true);
	}

	public void Hare(){
		Clear();
		hare.SetActive(true);
	}

	public void Saddle(){
		if (horse.activeSelf == true) {
			if (saddle.activeSelf == true) {
				saddle.SetActive (false);
		
			} else {
		
				saddle.SetActive (true);
			}
		}

	}

	public void Buffalo(){
		Clear();
		buffalo.SetActive(true);
	}

	public void Horse(){
		Clear();
		horse.SetActive(true);
	}

    public void Marlin()
    {
        Clear();
        marlin.SetActive(true);
    }

    public void Stag(){
        Clear();
        stag.SetActive(true);
    }

	public void Alligator(){
		Clear();
		alligator.SetActive(true);
	}
    
	public void Panther(){
		Clear();
		panther.SetActive(true);
	}

	public void Elephant(){
		Clear();
		elephant.SetActive(true);
	}

    public void Bear()
    {
        Clear();
        bear.SetActive(true);
    }

	void Clear(){
		sheep.SetActive(false);
		hare.SetActive(false);
		buffalo.SetActive(false);
        stag.SetActive(false);
        panther.SetActive(false);
		elephant.SetActive(false);
        bear.SetActive(false);
        marlin.SetActive(false);
		horse.SetActive(false);
		saddle.SetActive(false);
		alligator.SetActive(false);
    }

}
