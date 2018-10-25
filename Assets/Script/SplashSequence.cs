using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSequence : MonoBehaviour {
    
	void Start () {
        StartCoroutine(toMainScene());
	}

    IEnumerator toMainScene() { 
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
