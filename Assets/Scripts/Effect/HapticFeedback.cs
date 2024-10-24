using CandyCoded.HapticFeedback;
using UnityEngine;

public class HapstickFeedback : MonoBehaviour
{
    private void Start()
    {
        RangeWeapon.OnAnyShoot += Vibrate;
    }

    private void OnDestroy()
    {
        RangeWeapon.OnAnyShoot -= Vibrate;
    }

    private void Vibrate()
    {
        HapticFeedback.LightFeedback();
    }
}
