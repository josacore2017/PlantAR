using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryId : MonoBehaviour {

    public Text idSensor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void precionoIngresarId(Text id) {

        if (!id.Equals("")) {
            SceneManager.LoadSceneAsync("Main");
        }

    }
}
