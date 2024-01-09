using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoBusterUiController : MonoBehaviour
{

    //Declarations
    [SerializeField] private bool _isPlayer = false;


    //monobehaviors
    //...


    //Utilites

    //Extrtnal Control Utils
    public void SetupIsplayer()
    {
        _isPlayer = transform.parent.parent.GetComponent<ShipInformation>().IsPlayer();
    }

    public void ReduceUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetCargoBusterUiController().DrainSingle();
    }

    public void FillUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetCargoBusterUiController().FillSingle();
    }

    public void ReduceAllUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetCargoBusterUiController().DrainAll();
    }

    public void FillAllUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetCargoBusterUiController().FillAll();
    }

    public void DeactivateUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetCargoBusterUiController().GetComponent<DisplayAnimController>().HideDisplay();
    }

    public void ActivateUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetCargoBusterUiController().GetComponent<DisplayAnimController>().ShowDisplay();
    }

    public void EnterRegen()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetCargoBusterUiController().SetIsRegeneratingState(true);
    }

    public void ExitRegen()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetCargoBusterUiController().SetIsRegeneratingState(false);
    }

    public void TriggerPositiveEffectAndReset()
    {
        if (_isPlayer)
        {
            OldUiManager.Instance.GetCargoBusterUiController().GetComponent<DisplayAnimController>().TriggerPositiveEffect();
            Invoke("DeactivateUI", .35f);
            Invoke("ReduceAllUI", .35f);
        }
            
    }
}
