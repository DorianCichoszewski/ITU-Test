using System;
using ITUTest.Pathfinding;
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
		private TMP_Dropdown generationModeDropdown;
		[SerializeField]
		private Button generateMapButton;

		[SerializeField]
		private MapDisplay mapComponent;

		private void Awake()
		{
			generationModeDropdown.options.Clear();
			foreach (var generationModeName in Enum.GetNames(typeof(MapGenerationMode)))
			{
				generationModeDropdown.options.Add(new (generationModeName));
			}
			generateMapButton.onClick.AddListener(() =>
				mapComponent.GenerateMap(int.Parse(widthInput.text), int.Parse(heightInput.text),
					(MapGenerationMode) generationModeDropdown.value));
		}
	}
}