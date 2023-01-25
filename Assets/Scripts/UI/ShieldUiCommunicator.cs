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
    }

    public void ReduceShieldsUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetShieldsUiController().DrainSingle();
    }

    public void FillShieldsUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetShieldsUiController().FillSingle();
    }

    public void ReduceAllShieldsUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetShieldsUiController().DrainAll();
    }

    public void FillAllShieldsUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetShieldsUiController().FillAll();
    }

    public void DeactivateShieldsUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetShieldsUiController().gameObject.SetActive(false);
    }

    public void ActivateShieldsUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetShieldsUiController().gameObject.SetActive(true);
    }

    public void EnterRegen()
    {
        if (_isPlayer)
            UiManager.Instance.GetShieldsUiController().SetIsRegeneratingState(true);
    }

    public void ExitRegen()
    {
        if (_isPlayer)
            UiManager.Instance.GetShieldsUiController().SetIsRegeneratingState(false);
    }


}
