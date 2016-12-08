using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderActive : MonoBehaviour {
    public Shader shaderDisolve;
    public Renderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        shaderDisolve = Shader.Find("Disolving");
	}
	
	// Update is called once per frame
	void OnColliderEnter (Collision collision) {
		if(collision.gameObject.tag == "bloc")
        {
            rend.material.shader.
        }
	}
}
