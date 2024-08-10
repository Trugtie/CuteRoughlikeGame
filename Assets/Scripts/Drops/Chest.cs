using System;
using UnityEngine;

public class Chest : CurrencyDrop
{
    public static Action<Chest> OnAnyChestCollected;

    public override void Collect(Player player)
    {
        if (_isCollected)
            return;

        _isCollected = true;

        Collected();
    }

    protected override void Collected()
    {
        OnAnyChestCollected?.Invoke(this);
    }
}
