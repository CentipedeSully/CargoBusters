using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualController : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> _damageCollection;
    [SerializeField] private List<GameObject> _randomizedSelection;
    private bool _isFirstLvlDamageShowing = false;
    private bool _isSecondLvlDamageShowing = false;

    //Monobehaviors
    private void Awake()
    {
        CreateRandomizedSelectionList();
    }


    //Utilites
    private void CreateRandomizedSelectionList()
    {
        while (_randomizedSelection.Count < 2)
        {
            if (_damageCollection.Count < 1)
                break;
            else
            {
                int randomizedIndex = Random.Range(0, _damageCollection.Count);
                _randomizedSelection.Add(_damageCollection[randomizedIndex]);
                _damageCollection.RemoveAt(randomizedIndex);
            }
        }
    }

    private void ShowFirstDamageLvl()
    {
        _randomizedSelection[0].SetActive(true);
        _isFirstLvlDamageShowing = true;
    }

    private void FixFirstLvlDamage()
    {
        _randomizedSelection[0].SetActive(false);
        _isFirstLvlDamageShowing = false;
    }

    private void ShowSecondLvlDamage()
    {
        _randomizedSelection[1].SetActive(true);
        _isSecondLvlDamageShowing = true;
    }

    private void FixSecondLvlDamage()
    {
        _randomizedSelection[1].SetActive(false);
        _isSecondLvlDamageShowing = false;
    }


    //External Control Utils
   public void IncrementShipDamageSeverity()
    {
        if (_isFirstLvlDamageShowing == false)
            ShowFirstDamageLvl();
        else if (_isSecondLvlDamageShowing == false)
            ShowSecondLvlDamage();
    }

    public void DecrementShipDamageSeverity()
    {
        if (_isSecondLvlDamageShowing)
            FixSecondLvlDamage();
        else if (_isFirstLvlDamageShowing)
            FixFirstLvlDamage();
    }

    public void FixAllVisualDamage()
    {
        FixSecondLvlDamage();
        FixFirstLvlDamage();
    }

    public void ShowMaxDamageVisuals()
    {
        ShowFirstDamageLvl();
        ShowSecondLvlDamage();
    }

}
