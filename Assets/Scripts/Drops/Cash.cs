using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : CurrencyDrop
{
    public static Action<Cash> OnAnyCashCollected;
    protected override void Collected()
    {
        OnAnyCashCollected?.Invoke(this);
    }
}
