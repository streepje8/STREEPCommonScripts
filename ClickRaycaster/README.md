# ClickRaycaster
This script allows you to have clickable 3D objects on screen

## How to use
Add the ClickRaycaster script to the camera and select a layer for clickable objects.
Then on any csharp script change the : to add the interface IClickable
```
public class Name : Monobehaviour, IClickable
{
	void OnClick() 
	{
		Debug.Log("I got clicked!");
	}
}
```
