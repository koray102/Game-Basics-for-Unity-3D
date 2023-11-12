using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletReloadBar : MonoBehaviour
{
    public PlayerMovementPhysics playerSc;
    public Slider slider;

    public void SetShootTime (float value)
    {
        slider.value = playerSc.timeAfterShoot;
    }
}
