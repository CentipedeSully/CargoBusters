using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUiCommunicator : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isPlayer = false;


    //Monobhaviors
    //...


    //Utiliites
    //Extrtnal Control Utils
    public void SetupIsplayer()
    {
        _isPlayer = transform.parent.GetComponent<ShipInformation>().IsPlayer();
        ActivateHealthUI();
        int hullCount = (int)GetComponent<IntegrityBehavior>().GetMaxIntegrity();
        for (int i = 0; i < hullCount; i++)
            FillHealthUI();
    }

    public void ReduceHealthUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetHealthUiController().DrainSingle();
    }

    public void FillHealthUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetHealthUiController().FillSingle();
    }

    public void ReduceAllHealthUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetHealthUiController().DrainAll();
    }

    public void FillAllHealthUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetHealthUiController().FillAll();
    }

    public void DeactivateHealthUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetHealthUiController().GetComponent<DisplayAnimController>().HideDisplay();
    }

    public void ActivateHealthUI()
    {
        if (_isPlayer)
            UiManager.Instance.GetHealthUiController().GetComponent<DisplayAnimController>().ShowDisplay();
    }

    public void EnterRegen()
    {
        if (_isPlayer)
            UiManager.Instance.GetHealthUiController().SetIsRegeneratingState(true);
    }

    public void ExitRegen()
    {
        if (_isPlayer)
            UiManager.Instance.GetHealthUiController().SetIsRegeneratingState(false);
    }
}
