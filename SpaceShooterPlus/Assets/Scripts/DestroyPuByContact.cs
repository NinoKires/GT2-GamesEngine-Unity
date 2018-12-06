using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPuByContact : MonoBehaviour {

    public GameObject explosion;

    public int scoreValue;

    private GameController gameController;

	// Use this for initialization
	void Start () {

        GameObject gameControllerObj = GameObject.FindGameObjectWithTag("GameController");

        if (gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }

        if (gameControllerObj == null)
        {
            Debug.Log("Cannot find GameController");
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Bolt"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Player"))
        {
            //apply powerup
            gameObject.GetComponent<PowerUp>().Powerup(other.gameObject);
            gameController.AddScore(scoreValue);
            Destroy(gameObject);
            return;
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
