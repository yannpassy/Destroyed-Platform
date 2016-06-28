using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class move : MonoBehaviour
{
    /*private Transform myTransform;            // this transform
    private Vector3 destinationPosition;      // The destination Point*/
    private float destinationDistance;         // The distance between myTransform and destinationPosition
	public Vector3 positionDepart, positionArrivee, direction;
	public GameObject perso;

    public float moveSpeed;      // The Speed the character will move
	int layerMask;
    public float speed;

	public Camera camera1;
	public GameObject cube;

    void Start()
    {
        /*myTransform = transform;                     // sets myTransform to this GameObject.transform
        destinationPosition = myTransform.position;         // prevents myTransform reset*/
		layerMask = 1 << 8;
		perso = GameObject.Find ("perso");
		positionArrivee = transform.position;

    }


    // Update is called once per frame
    void Update()
    {
		destinationDistance = Vector3.Distance(transform.position, positionArrivee);
		cube = GameObject.Find ("Cube");
		//cube.transform.position = positionArrivee;

        /*if (destinationDistance < .5f)
        {      // To prevent shakin behavior when near destination
            moveSpeed = 0;
        }
        else if (destinationDistance > .5f)
        {         // To Reset Speed to default
            moveSpeed = speed;
        }*/

    
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
           /* Vector3 targetPoint = ray.GetPoint(hitdist);
            destinationPosition = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            myTransform.rotation = targetRotation;
			camera1.transform.LookAt (ray.GetPoint (hitdist));*/
			positionArrivee = hit.point;
			direction = positionArrivee - transform.position;
        }


        // To prevent code from running if not needed
        if (destinationDistance > .5)
        {
            //myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
			transform.position += direction * speed;
        }
    }
}