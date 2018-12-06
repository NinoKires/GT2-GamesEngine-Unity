using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireratePU : PowerUp {

    protected override void ApplyEffect(PlayerController playerController)
    {
        playerController.fireRate *= 0.9f;
    }
}
