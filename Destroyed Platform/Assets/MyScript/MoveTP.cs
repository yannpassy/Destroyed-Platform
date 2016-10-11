using UnityEngine;
using System.Collections;

public class MoveTP : MonoBehaviour {
	public Texture ecranNoirTexture;
	public float vitesseFondu;

	private enum Etat {Look, AnalyseCommande, fadeOut, teleportation, fadeIn, demiTour};
	Etat etat;
	private int drawdepth;
	private float alpha;
	private int fadeDir;
	private Vector3 centreCamera;
	private Vector3 nouvellePosition;
	int mask;
	private double chrono;
	public GameObject perso;

	private Vector3 anciennePositionCube;
	public OVRCameraRig cameraOVR;
	public GameObject cube;
    public string tagTouche;

    /// <summary>
    /// How long it takes to fade.
    /// </summary>
    public float fadeTime = 2.0f;
	private float dist;

	/// <summary>
	/// The initial screen color.
	/// </summary>
	public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);

	private Material fadeMaterial = null;
	private bool isFading = false;
	private YieldInstruction fadeInstruction = new WaitForEndOfFrame();

	/// <summary>
	/// Initialize.
	/// </summary>
	void Awake()
	{
		// create the fade material
		fadeMaterial = new Material(Shader.Find("Oculus/Unlit Transparent Color"));
	}


	void Start() {
		mask = (1 << 8);
		vitesseFondu = 0.8f;
		drawdepth = -1000;
		alpha = 1.0f;
		fadeDir = -1;
		centreCamera = new Vector3 (Screen.width / 2.0f, Screen.height / 2.0f, cameraOVR.transform.forward.z);
		etat = Etat.Look;
	}
		


	// Update is called once per frame
	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
        RaycastHit hit;
		dist = Vector3.Distance (anciennePositionCube, cube.transform.position);

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			nouvellePosition = hit.point;
			cube.transform.position = nouvellePosition;
            tagTouche = hit.collider.tag;
		}

		if (etat == Etat.Look) {
			if (dist < 5.0f) {
				chrono += Time.deltaTime;
			}
			else{
				chrono = 0;
				anciennePositionCube = nouvellePosition;
				Debug.Log ("ancienne position :" +anciennePositionCube + "current position :" +nouvellePosition + "position cube : " + cube.transform.position);
			}

			if (chrono > 2) {
                //etat = Etat.teleportation;
                etat = Etat.AnalyseCommande;
            }
		}
		else if (etat == Etat.AnalyseCommande) {
            /*this.transform.position = new Vector3(nouvellePosition.x, nouvellePosition.y+0.6f, nouvellePosition.z);
			cameraOVR.transform.position = new Vector3(nouvellePosition.x, nouvellePosition.y+1.0f, nouvellePosition.z);
			etat = Etat.Look;*/
            
            if (tagTouche == "demi-tour")
            {
                etat = Etat.demiTour;
            }
            else
            etat = Etat.Look;
		}
        else if (etat == Etat.demiTour)
        {
            perso.transform.rotation *= Quaternion.AngleAxis(180 , Vector3.up);
            cameraOVR.transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            chrono = 0;
            etat = Etat.Look;
        }
        

        }
		

	/*IEnumerator teleportation (Vector3 nouvellePostion)
	{
		//fadeDir = vitesse;
		//if (alpha < 1)
		yield return new WaitForSeconds(0.5f);
		perso.transform.position = nouvellePostion;
		etat = Etat.fadeIn;
		//fadeDir = -vitesse;

	}*/

	/*void OnGUI()
	{
		alpha += fadeDir * vitesseFondu * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawdepth;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ecranNoirTexture);
	}*/

	/// <summary>
	/// Fades alpha from 1.0 to 0.0
	/// </summary>
	IEnumerator FadeIn()
	{
		float elapsedTime = 0.0f;
		fadeMaterial.color = fadeColor;
		Color color = fadeColor;
		isFading = true;
		while (elapsedTime < fadeTime)
		{
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime;
			color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
			fadeMaterial.color = color;
		}
		isFading = false;
	}

	IEnumerator FadeOut()
	{
		float elapsedTime = 0.0f;
		Color color = fadeColor;
		color.a = 0f;
		fadeMaterial.color = color;
		isFading = true;
		while (elapsedTime < fadeTime)
		{
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime;
			color.a = Mathf.Clamp01(elapsedTime / fadeTime);
			fadeMaterial.color = color;
		}
		isFading = false;
	}

	/// <summary>
	/// Renders the fade overlay when attached to a camera object
	/// </summary>
	void OnPostRender()
	{
		if (isFading)
		{
			fadeMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Color(fadeMaterial.color);
			GL.Begin(GL.QUADS);
			GL.Vertex3(0f, 0f, -12f);
			GL.Vertex3(0f, 1f, -12f);
			GL.Vertex3(1f, 1f, -12f);
			GL.Vertex3(1f, 0f, -12f);
			GL.End();
			GL.PopMatrix();
		}
	}

}