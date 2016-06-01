using UnityEngine;
using System.Collections;

public class Deplacement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //avancer
        if (Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(new Vector3(0,0,5) * Time.deltaTime);
        }
        //reculer
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, 0,-5) * Time.deltaTime);
        }
        //tourne vers la droite
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
        }
        //tourne vers la gauche
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -50, 0) * Time.deltaTime);
        }

    }
}
