using UnityEngine;

public class GameObjectSwapper : MonoBehaviour
{
    public GameObject Original;
    // Use this for initialization

	public void Swap()
	{
		transform.position = Original.transform.position;
		transform.rotation = Original.transform.rotation;
		transform.localScale = Original.transform.localScale;
		
		Original.SetActive(false);
		gameObject.SetActive(true);
	}
}
