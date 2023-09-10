using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpUiCommunicator : MonoBehaviour
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

    public void ReduceUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetWarpUiController().DrainSingle();
    }

    public void FillUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetWarpUiController().FillSingle();
    }

    public void ReduceAllUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetWarpUiController().DrainAll();
    }

    public void FillAllUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetWarpUiController().FillAll();
    }

    public void DeactivateUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetWarpUiController().GetComponent<DisplayAnimController>().HideDisplay();
    }

    public void ActivateUI()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetWarpUiController().GetComponent<DisplayAnimController>().ShowDisplay();
    }

    public void EnterRegen()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetWarpUiController().SetIsRegeneratingState(true);
    }

    public void ExitRegen()
    {
        if (_isPlayer)
            OldUiManager.Instance.GetWarpUiController().SetIsRegeneratingState(false);
    }

    public void TriggerPositiveEffectAndReset()
    {
        if (_isPlayer)
        {
            OldUiManager.Instance.GetWarpUiController().GetComponent<DisplayAnimController>().TriggerPositiveEffect();
            Invoke("DeactivateUI", .35f);
            Invoke("ReduceAllUI", .35f);
        }

    }
}
