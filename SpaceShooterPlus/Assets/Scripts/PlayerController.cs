
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public Boundary boundary;

    public float speed;
    public float tilt;

    public float fireRate;
    public GameObject shot;
    public GameObject shotSpawn;

    public int bonusShots;
    private const float spread = 0.5f;
    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {

            nextFire = Time.time + fireRate;
            GameObject newBolt = Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
            if (bonusShots > 0)
            {
                for (int i = 1; i <= bonusShots; i++)
                {
                    newBolt = (GameObject)Instantiate(shot, shotSpawn.transform.position + Vector3.left * i * spread, shotSpawn.transform.rotation);

                    newBolt = (GameObject)Instantiate(shot, shotSpawn.transform.position + Vector3.right * i * spread, shotSpawn.transform.rotation);
                }
            }
            GetComponent<AudioSource>().Play();

        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
