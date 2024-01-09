using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    //Declarations
    [Header("References")]
    [SerializeField] private AbstractShip _currentLinkedShip;

    [Header("Screen Objects")]
    [SerializeField] private List<GameObject> _activeScreens;
    [SerializeField] private GameObject _shipHUDScreen;

    public delegate void UiManagerEvent();
    public event UiManagerEvent OnShipDelinkedFromUi;
    public event UiManagerEvent OnNewShipLinkedToUi;




    //Monobehaviours
    private void Awake()
    {
        _activeScreens = new List<GameObject>();
    }






    //Internal Utils
    private void DelinkFromCurrentShip()
    {
        //Unsubscribe references from current ship
        OnShipDelinkedFromUi?.Invoke();
    }

    private void LinkToShip(AbstractShip ship)
    {
        //Subscribe references to new current ship
        //Also Update the UI to reflect the new ship's info
        OnNewShipLinkedToUi?.Invoke();
    }


    private void ActivateScreen(GameObject screenObject)
    {
        if (screenObject == null)
            Debug.LogError("UiManager tried to activate a null screen object");

        else if (!_activeScreens.Contains(screenObject))
        {
            screenObject.SetActive(true);
            _activeScreens.Add(screenObject);
        }
    }

    private void DeactivateScreen(GameObject screenObject)
    {
        if (screenObject == null)
            Debug.LogError("UiManager tried to deactivate a null screen object");

        else if (_activeScreens.Contains(screenObject))
        {
            screenObject.SetActive(false);
            _activeScreens.Remove(screenObject);
        } 
    }

    

    //Getters, Setters, & Commands
    public void LinkUiToShip(AbstractShip newShip)
    {
        if (_currentLinkedShip == null)
            LinkToShip(newShip);

        else if (_currentLinkedShip != newShip)
        {
            DelinkFromCurrentShip();
            LinkToShip(newShip);
        }
    }

    public List<GameObject> GetActiveScreens()
    {
        return _activeScreens;
    }

    public AbstractShip GetCurrentlyLinkedShip()
    {
        return _currentLinkedShip;
    }

    public void ActivateShipHudScreen()
    {
        ActivateScreen(_shipHUDScreen);
    }

    public void DeactivateShipHudScreen()
    {
        DeactivateScreen(_shipHUDScreen);
    }


}
