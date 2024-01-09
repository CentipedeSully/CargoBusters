using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Declarations
    [SerializeField] private AbstractShip _playerShipRef;

    public delegate void PlayerManagementEvent();
    public event PlayerManagementEvent OnPlayerShipAdded;
    public event PlayerManagementEvent OnPlayerShipRemoved;



    //Monobehaviours




    //Internal Utils




    //Getters, Setters & Commands
    public AbstractShip GetPlayerShip()
    {
        return _playerShipRef;
    }

    public bool DoesPlayerShipExist()
    {
        return _playerShipRef != null;
    }

    public void ClearPlayerShip()
    {
        if (_playerShipRef != null)
        {
            _playerShipRef = null;
            OnPlayerShipRemoved?.Invoke();
        }
    }

    public void SetShipAsPlayer(AbstractShip ship)
    {
        if (ship != null)
        {
            _playerShipRef = ship;
            OnPlayerShipAdded?.Invoke();
        }
            
    }


}
