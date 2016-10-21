using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MoveTP : MonoBehaviour {
	public Transform objectReference;
	public OVRCameraRig cameraOVR;
	private enum Etat {Look, AnalyseCommande, fadeOut, teleportation, fadeIn, demiTour, resetSizeCursur};
	Etat etat;
	private Vector3 centreCamera;
	private Vector3 nouvellePosition;
	private double chrono;
	private double chronoFadeOut;
	private double chronoFadeIn;
	private string tagTouchee;
	private OVRScreenFadeOut fadeout;
	private OVRScreenFadeIn fadein;

	private Vector3 anciennePositionCube, scaleCurseur, scaleValidation;
	public Camera cam;
	public GameObject cube;

	private float dist;
	void Start(){
		centreCamera = new Vector3 (Screen.width / 2.0f, Screen.height / 2.0f, cameraOVR.transform.forward.z);
		etat = Etat.Look;
        scaleCurseur = new Vector3(0.3f, 0.3f, 0.3f);
        scaleValidation = new Vector3(0.05f, 0.17f, 0.05f);

    }

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
			if (dist < 0.1f) {
				chrono += Time.deltaTime;
			} else{
                chrono = 0;
                anciennePositionCube = nouvellePosition;
                etat = Etat.resetSizeCursur;
            }

            if (chrono > 1)
            {
                cube.transform.DOScale(scaleValidation, 1);
            }

            if (chrono > 2) {
				etat = Etat.AnalyseCommande;
			}
		} 
		else if (etat == Etat.AnalyseCommande) {
            Debug.Log(tagTouchee);
			if (tagTouchee == "terrain") {
				etat = Etat.fadeOut;
			}
			if (tagTouchee == "obstacle") {
                chrono = 0;
                etat = Etat.resetSizeCursur;
			}
			if (tagTouchee == "demi-tour") {
				etat = Etat.demiTour;
			}
		} 
		else if (etat == Etat.fadeOut) {
			cam.GetComponent<OVRScreenFadeOut> ().enabled = true;
			chronoFadeOut += Time.deltaTime;
			if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
				cam.GetComponent<OVRScreenFadeOut> ().enabled = false;
				etat = Etat.teleportation;
				chronoFadeOut = 0;
			}
		} 
		else if (etat == Etat.teleportation) {
            cube.transform.DOScale(scaleCurseur, 0.01f);
			this.transform.position = new Vector3 (nouvellePosition.x, nouvellePosition.y+1.0f, nouvellePosition.z);
			cameraOVR.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+0.6f, this.transform.position.z);
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
            cube.transform.DOScale(scaleCurseur, 0.01f);
            chrono = 0;
            etat = Etat.Look;
		}
         else if(etat == Etat.resetSizeCursur){
            cube.transform.DOScale(scaleCurseur, 0.01f);
            chrono += Time.deltaTime;
            if (chrono > 1)
            {
                chrono = 0;
                etat = Etat.Look;
            }
        }


    }

}