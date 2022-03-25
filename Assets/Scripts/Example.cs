using UnityEngine;

public class Example : MonoBehaviour
{
	
	//public bool wasLocked = 1;
	public float wasLocked = 0;
	void Start()
	{
		Screen.lockCursor = true;
	}

	// Called when the cursor is actually being locked

	void DidLockCursor()
	{
		Debug.Log("Locking cursor");
		Screen.lockCursor = true;
		// Disable the button
		wasLocked = 1;
	}

	// Called when the cursor is being unlocked
	// or by a script calling Screen.lockCursor = false;
	void DidUnlockCursor()
	{
		Debug.Log("Unlocking cursor");
		Screen.lockCursor = false;
		// Show the button again
		wasLocked = 0;
	}
	
	void Update()
	{
		


		// In standalone player we have to provide our own key
		// input for unlocking the cursor
		if (Input.GetKeyDown ("escape") && wasLocked==1) {
			
		
			DidUnlockCursor ();
		}
		else
		{ if  (Input.GetKeyDown ("escape") && wasLocked==0) {
			
		
			DidLockCursor ();
		}
		}
		// Did we lose cursor locking?
		// eg. because the user pressed escape
		// or because they switched to another application
		// or because some script set Screen.lockCursor = false;

	}
}