using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMovementController : MonoBehaviour
{
    public string LeaveSeatInput;
    public CinemaController SittingController;
    public CinemaController SittingUpController;
    public Player Player;

    public bool LockMovement;
    public bool LockView;

    private void Start()
    {
        enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonUp(LeaveSeatInput))
        {
            SitUp();
            enabled = false;
        }
    }

    public void SitDownOnObject()
    {
        Player.Controller.enabled = false;
        Player.FirstPerson.enabled = false;

        SittingController.TransitionToTargetAndAnimate(() => 
		{
			Player.Controller.enabled = !LockMovement;
			Player.FirstPerson.ReInitializeMouseLook();
			Player.FirstPerson.enabled = !LockView;
			enabled = true;
		});
    }

    public void SitUp()
    {
        SittingUpController.AnimateAndTransitionToTarget(() =>
        {
            Player.Controller.enabled = true;
			Player.FirstPerson.ReInitializeMouseLook();			
            Player.FirstPerson.enabled = true;
        });

    }
}
