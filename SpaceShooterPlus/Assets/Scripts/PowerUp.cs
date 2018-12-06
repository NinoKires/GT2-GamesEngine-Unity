using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

    public void Powerup(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        ApplyEffect(playerController);
    }

    protected abstract void ApplyEffect(PlayerController player);


}
