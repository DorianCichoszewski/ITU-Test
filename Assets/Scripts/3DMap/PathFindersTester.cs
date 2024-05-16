using System;
using System.Collections;
using System.Collections.Generic;
using ITUTest.Pathfinding;
using ITUTest.Pathfinding.Algorithm;
using UnityEditor;
using UnityEngine;

namespace ITUTest._3DMap
{
	public class PathFindersTester : MonoBehaviour
	{
		[SerializeField]
		private int width, height;

		[SerializeField]
		private int testCount = 100;

		private Map generatedMap;

		private readonly List<(Node start, Node end)> randomNodes = new();

		IEnumerator Start()
		{
			GenerateTestData();

			// Wait for unity to stabilise
			yield return new WaitForSeconds(2);
			TestAlgorithm(new DijkstraPathfinder(generatedMap));
			yield return null;
			GC.Collect();
			yield return null;
			TestAlgorithm(new AStarPathfinder(generatedMap));
			yield return null;
			GC.Collect();
			yield return null;
			TestAlgorithm(new BestFirstSearchPathfinder(generatedMap));
			yield return null;
			GC.Collect();
			yield return null;
#if UNITY_EDITOR
			EditorApplication.isPaused = true;
#endif
		}

		private void GenerateTestData()
		{
			generatedMap = new Map(width, height);

			for (int i = 0; i < testCount; i++)
			{
				var start = generatedMap.GetRandomNode();
				var end = generatedMap.GetRandomNode();

				randomNodes.Add((start, end));
			}
		}

		private void TestAlgorithm(IPathfindingAlgorithm algorithm)
		{
			for (int i = 0; i < testCount; i++)
			{
				algorithm.FindPath(randomNodes[i].start, randomNodes[i].end);

				//Debug.Log(algorithm.FindPath(randomNodes[i].start, randomNodes[i].end));
			}
		}
	}
}