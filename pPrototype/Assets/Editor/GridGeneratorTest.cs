using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class GridGeneratorTest 
{

	[Test]
	//[Ignore]
	public void EmptyGridTest()
	{
		var grid = GridGenerator.Create();
	}
}
