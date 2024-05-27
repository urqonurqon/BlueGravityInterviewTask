using UnityEngine;

public class GateTrigger : MonoBehaviour
{

	[SerializeField] private GameObject _gate;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_gate.SetActive(false);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_gate.SetActive(true);
		}
	}
}
