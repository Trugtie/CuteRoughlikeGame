using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : CurrencyDrop
{
    public static Action<Candy> OnAnyCandyCollected;
    protected override void Collected()
    {
        OnAnyCandyCollected?.Invoke(this);
    }
}
