using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipOccupancyManager : MonoBehaviour
{
    //Declarations
    [SerializeField] private AbstractShip _currentPlayerShip;



    //Monobehaviours





    //Internal Utils
    private void RelenquishAllControlFromShip()
    {

    }

    private void MakeShipControllableByPlayer(AbstractShip shipRef)
    {
        if (shipRef != _currentPlayerShip)
        {

        }
    }

    private void MakeShipControllableByAi(AbstractShip shipRef)
    {

    }




    //Gettersm Setters, & Commands
    public void OccupyShipAsPlayer(AbstractShip shipRef)
    {
        if (shipRef != null)
        {
            MakeShipControllableByPlayer(shipRef);
            
        }
        

    }

    public void OccupyShipAsAi(AbstractShip shipRef)
    {

    }

    public void FollowAiShip(AbstractShip shipRef)
    {

    }



}
