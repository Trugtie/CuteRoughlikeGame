using System;
using System.Collections.Generic;
using Tabsil.Sijil;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour, IWantToBeSaved
{
    private const string UNLOCK_LIST = "UnlockList";
    private const string LAST_SELECTED_CHARACTER_INDEX = "LastSelectedCharacterSelected";

    public static Action<CharacterDataSO> OnSelectedCharacterAction;

    public static CharacterSelectionManager Instance { get; private set; }

    public CharacterDataSO[] CharactersDataSO { get; private set; }

    public int SelectedIndex { get; private set; }
    public List<bool> UnlockedList { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public CharacterDataSO SelectCharacter(int index)
    {
        SelectedIndex = index;
        Save();

        if (UnlockedList[index])
            OnSelectedCharacterAction?.Invoke(CharactersDataSO[index]);

        return CharactersDataSO[index];
    }

    public CharacterDataSO PurchaseCharacter()
    {
        int price = CharactersDataSO[SelectedIndex].PurchasePrice;
        CurrencyManager.Instance.UsePremiumCurrency(price);
        UnlockedList[SelectedIndex] = true;
        Save();

        return CharactersDataSO[SelectedIndex];
    }

    public void Load()
    {
        CharactersDataSO = ResourceManager.Characters;

        UnlockedList = new List<bool>();

        for (int i = 0; i < CharactersDataSO.Length; i++)
            UnlockedList.Add(i == 0);

        if (Sijil.TryLoad(this, UNLOCK_LIST, out object unlockListObject))
            UnlockedList = (List<bool>)unlockListObject;

        if (Sijil.TryLoad(this, LAST_SELECTED_CHARACTER_INDEX, out object lastSelectedCharacterIndexObject))
            SelectedIndex = (int)lastSelectedCharacterIndexObject;
    }

    public void Save()
    {
        Sijil.Save(this, UNLOCK_LIST, UnlockedList);
        Sijil.Save(this, LAST_SELECTED_CHARACTER_INDEX, SelectedIndex);
    }
}
