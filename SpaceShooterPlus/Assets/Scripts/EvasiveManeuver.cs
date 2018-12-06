using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWait;
    public Vector2 maneuverTIme;
    public Vector2 maneuverWait;

    public float dodge;
    public float smoothing;
    public float tilt;

    public Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;

    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);   //the "sign" - pos or neg - of the value in arg: opposite sign value of x position ..
            yield return new WaitForSeconds(Random.Range (maneuverTIme.x, maneuverTIme.y));//.. if enemy is on +1 site sign will multiply it with -1; youll always dodge inwards
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        //will keep us inside the boundary
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
