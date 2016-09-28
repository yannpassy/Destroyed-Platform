using UnityEngine;
using System.Collections;

public class MoveTP : MonoBehaviour {
    public Texture ecranNoirTexture;
    public float vitesseFondu = 0.8f;

    private int drawdepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;  
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            StartCoroutine(teleportation(10, new Vector3(-0.59f, 1.0f, 15.1f)));
        }

    }

    void OnGUI()
    {
        alpha += fadeDir * vitesseFondu * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawdepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ecranNoirTexture);
    }

    IEnumerator teleportation (int vitesse, Vector3 nouvellePostion)
    {
        fadeDir = vitesse;
        if (alpha < 1)
            yield return new WaitForSeconds(0.5f);
        transform.position = nouvellePostion;
        fadeDir = -vitesse;

    }

}
