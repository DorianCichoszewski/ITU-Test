using ITUTest.MapView;
using UnityEngine;
using UnityEngine.UI;

namespace ITUTest.UI
{
	public class ClickActionResolver : MonoBehaviour
	{
		[SerializeField]
		private Toggle changeNodeTypeToggle;

		[SerializeField]
		private Toggle setStartToggle;

		[SerializeField]
		private Toggle setEndToggle;

		[Space]
		[SerializeField]
		private NodeRaycast raycaster;

		[SerializeField]
		private PathOnMap pathOnMap;

		private void Awake()
		{
			changeNodeTypeToggle.onValueChanged.AddListener(newValue =>
			{
				if (newValue)
					raycaster.OnNodeSelected += ChangeNodeType;
				else
					raycaster.OnNodeSelected -= ChangeNodeType;
			});
			setStartToggle.onValueChanged.AddListener(newValue =>
			{
				if (newValue)
					raycaster.OnNodeSelected += SetStartPoint;
				else
					raycaster.OnNodeSelected -= SetStartPoint;
			});
			setEndToggle.onValueChanged.AddListener(newValue =>
			{
				if (newValue)
					raycaster.OnNodeSelected += SetEndPoint;
				else
					raycaster.OnNodeSelected -= SetEndPoint;
			});
		}

		private void ChangeNodeType(NodeObject node)
		{
			node.ToggleNodeType();
			pathOnMap.CalculatePath();
		}

		private void SetStartPoint(NodeObject node)
		{
			pathOnMap.SetStartPoint(node);
		}

		private void SetEndPoint(NodeObject node)
		{
			pathOnMap.SetEndPoint(node);
		}
	}
}