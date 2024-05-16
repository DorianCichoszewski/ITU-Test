using System;
using ITUTest._3DMap;
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
			widthInput.onValueChanged.AddListener(_ => CheckIfCanGenerate());
			heightInput.onValueChanged.AddListener(_ => CheckIfCanGenerate());
			generateMapButton.onClick.AddListener(() =>
				mapComponent.GenerateMap(int.Parse(widthInput.text), int.Parse(heightInput.text),
					(MapGenerationMode) generationModeDropdown.value));
			
			CheckIfCanGenerate();
		}

		private void CheckIfCanGenerate()
		{
			bool canGenerate = true;
			canGenerate &= int.TryParse(widthInput.text, out int width);
			canGenerate &= width > 0;
			canGenerate &= int.TryParse(heightInput.text, out int height);
			canGenerate &= height > 0;
			generateMapButton.interactable = canGenerate;
		}
	}
}