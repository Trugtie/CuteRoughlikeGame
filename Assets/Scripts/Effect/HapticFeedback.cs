using UnityEngine;

public class HapticFeedback : MonoBehaviour
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
        CandyCoded.HapticFeedback.HapticFeedback.MediumFeedback();
    }
}
