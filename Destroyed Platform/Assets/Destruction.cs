using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Destruction : MonoBehaviour {
	private double chrono;
	private GameObject cube; 
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		chrono += Time.deltaTime;
		GameObject.Find ("Cube").transform.DOShakeRotation (2,10,2,10, true);
		if (chrono > 2) {
			GameObject.Find ("Cube").transform.DOMoveY (-100, 20);
		}
		if (chrono > 4) {
			GameObject.Find ("Cube1").transform.DOMoveY (-100, 20);
		}
		if (chrono > 6) {
			GameObject.Find ("Cube3").transform.DOMoveY (-100, 20);
		}
		if (chrono > 8) {
			GameObject.Find ("Cube5").transform.DOMoveY (-100, 20);
		} 
		if (chrono > 10) {
			GameObject.Find ("Cube2").transform.DOMoveY (-100, 20);
		}
		if (chrono > 12) {
			GameObject.Find ("Cube6").transform.DOMoveY (-100, 20);
		}
		if (chrono > 14) {
			GameObject.Find ("Cube2").transform.DOMoveY (-100, 20);
		}
		if (chrono > 16) {
			GameObject.Find ("Cube4").transform.DOMoveY (-100, 20);
		}
		if (chrono > 18) {
			GameObject.Find ("Cube7").transform.DOMoveY (-100, 20);
		}
		if (chrono > 20) {
			GameObject.Find ("Cube9").transform.DOMoveY (-100, 20);
		}
	}
}