using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ITUTest.UI
{
	public class MapParameters : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField widthInput;
		[SerializeField]
		private TMP_InputField heightInput;
		[SerializeField]
		private Button generateMapButton;

		[SerializeField]
		private MapDisplay mapComponent;

		private void Start()
		{
			generateMapButton.onClick.AddListener(() =>
				mapComponent.GenerateMap(int.Parse(widthInput.text), int.Parse(heightInput.text)));
		}
	}
}