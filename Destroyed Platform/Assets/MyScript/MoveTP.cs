using UnityEngine;
using System.Collections;

public class MoveTP : MonoBehaviour {
	public Transform objectReference;
	public OVRCameraRig cameraOVR;
	private enum Etat {Look, AnalyseCommande, fadeOut, teleportation, fadeIn, demiTour};
	Etat etat;
	private Vector3 centreCamera;
	private Vector3 nouvellePosition;
	private double chrono;
	private double chronoFadeOut;
	private double chronoFadeIn;
	private string tagTouchee;
	private OVRScreenFadeOut fadeout;
	private OVRScreenFadeIn fadein;

	private Vector3 anciennePositionCube;
	public Camera cam;
	public GameObject cube;

	private float dist;


	// Update is called once per frame
	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
		RaycastHit hit;
		dist = Vector3.Distance (anciennePositionCube, cube.transform.position);

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			nouvellePosition = hit.point;
			cube.transform.position = nouvellePosition;
			tagTouchee = hit.collider.tag;
		}

		if (etat == Etat.Look) {
			if (dist < 1.0f) {
				chrono += Time.deltaTime;
			} else {
				chrono = 0;
				anciennePositionCube = nouvellePosition;
			}

			if (chrono > 2) {
				//etat = Etat.teleportation;
				etat = Etat.AnalyseCommande;
			}
		} 
		else if (etat == Etat.AnalyseCommande) {
			if (tagTouchee == "terrain") {
				etat = Etat.fadeOut;
			}
			if (tagTouchee == "obstacle") {
				chrono = 0;
				etat = Etat.Look;
			}
			if (tagTouchee == "demi-tour") {
				etat = Etat.demiTour;
			}
		} 
		else if (etat == Etat.fadeOut) {
			Debug.Log ("c'est bon");
			cam.GetComponent<OVRScreenFadeOut> ().enabled = true;
			chronoFadeOut += Time.deltaTime;
			if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
				Debug.Log ("c'est bon");
				cam.GetComponent<OVRScreenFadeOut> ().enabled = false;
				etat = Etat.teleportation;
				chronoFadeOut = 0;
			}
		} 
		else if (etat == Etat.teleportation) {
			this.transform.position = new Vector3 (nouvellePosition.x, nouvellePosition.y + 0.6f, nouvellePosition.z);
			cameraOVR.transform.position = new Vector3 (nouvellePosition.x, nouvellePosition.y + 1.0f, nouvellePosition.z);
			etat = Etat.fadeIn;
		} 
		else if (etat == Etat.fadeIn) {
			cam.GetComponent<OVRScreenFadeIn> ().enabled = true;
			chronoFadeIn += Time.deltaTime;
			if (chronoFadeIn > cam.GetComponent<OVRScreenFadeIn> ().fadeTime) {
				cam.GetComponent<OVRScreenFadeIn> ().enabled = false;
				chronoFadeIn = 0;
				etat = Etat.Look;
			}
		}
		else if (etat == Etat.demiTour) {
			this.transform.rotation *= Quaternion.AngleAxis (180, Vector3.up);
			cameraOVR.transform.rotation *= Quaternion.AngleAxis (180, Vector3.up);
			chrono = 0;
			etat = Etat.Look;
		}
        

	}

}