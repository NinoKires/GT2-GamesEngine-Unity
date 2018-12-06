using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotPU : PowerUp {

    private const int maxStack = 4;
    private float time = 5.0f;

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
    }

    protected override void ApplyEffect(PlayerController player)
    {

        player.bonusShots = Mathf.Clamp(player.bonusShots + 1, 0, maxStack);

        //if (time > 0)
        //{
        //    player.bonusShots = Mathf.Clamp(player.bonusShots + 1, 0, maxStack);
        //}

        //if (time == 0)
        //{
        //    player.bonusShots = Mathf.Clamp(player.bonusShots - 1, 0, maxStack);
        //}
    }


}
