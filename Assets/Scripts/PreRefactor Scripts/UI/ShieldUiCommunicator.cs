using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUiCommunicator : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isPlayer = false;


    //monobehaviors
    //...


    //Utiliites
    //Extrtnal Control Utils
    public void SetupIsplayer()
    {
        _isPlayer = transform.parent.GetComponent<ShipInformation>().IsPlayer();
        ActivateShieldsUI();
    }

    public void ReduceShieldsUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetShieldsUiController().DrainSingle();
    }

    public void FillShieldsUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetShieldsUiController().FillSingle();
    }

    public void ReduceAllShieldsUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetShieldsUiController().DrainAll();
    }

    public void FillAllShieldsUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetShieldsUiController().FillAll();
    }

    public void DeactivateShieldsUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetShieldsUiController().GetComponent<DisplayAnimController>().HideDisplay();
    }

    public void ActivateShieldsUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetShieldsUiController().GetComponent<DisplayAnimController>().ShowDisplay();
    }

    public void EnterRegen()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetShieldsUiController().SetIsRegeneratingState(true);
    }

    public void ExitRegen()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetShieldsUiController().SetIsRegeneratingState(false);
    }


}
