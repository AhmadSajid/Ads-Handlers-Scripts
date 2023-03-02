using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunselection : MonoBehaviour
{
	public int num;
	[SerializeField] private GameObject[] _tiers;
	[SerializeField] private Button _nextbtn;
	[SerializeField] private Button _previousbtn;
    //[SerializeField] private GameObject _obj;

    private void Start()
    {
		if (num == 0)
		{ _previousbtn.interactable = false; }

	}

    public void nextCat()
	{
		UISoundController.Instance.playSFX("click");

		if (num < _tiers.Length)
		{ num++; _nextbtn.interactable = true; _previousbtn.interactable = true; }
		else
		{ _nextbtn.interactable = false; }

		foreach (GameObject t in _tiers)
			t.SetActive(false);

		_tiers[num].SetActive(true);
		if (num == _tiers.Length-1)
		{ _nextbtn.interactable = false; }
	}

	public void previousCat()
	{
		UISoundController.Instance.playSFX("click");

		if (num >= 1)
		{ num--; _nextbtn.interactable = true; _previousbtn.interactable = true; }
		else
		{ _previousbtn.interactable = false; }

		foreach (GameObject t in _tiers)
			t.SetActive(false);

		_tiers[num].SetActive(true);
		if (num == 0)
		{ _previousbtn.interactable = false; }
	}
}
