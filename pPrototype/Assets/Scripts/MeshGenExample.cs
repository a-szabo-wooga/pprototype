using UnityEngine;

public class MeshGenExample : MonoBehaviour
{
	public Material QuadMaterial;

	#region Unity lifeCycle
	private void Start()
	{
		var quad = CreateQuad(2f, 2f);
	}
	#endregion

	private GameObject CreateQuad(float width, float height)
	{
		var quadObject = new GameObject("quad");

		var meshFilter = quadObject.AddComponent<MeshFilter>();
		var meshRenderer = quadObject.AddComponent<MeshRenderer>();

		meshFilter.mesh = CreateQuadMesh(width, height);
		meshRenderer.material = QuadMaterial;

		return quadObject;
	}

	private Mesh CreateQuadMesh(float width, float height)
	{
		var mesh = new Mesh();

		mesh.vertices = CreateQuadVertexArray(width, height);
		mesh.triangles = CreateQuadTriangles();
		mesh.normals = CreateQuadNormals();
		mesh.uv = CreateQuadUV();

		return mesh;
	}

	private Vector2[] CreateQuadUV()
	{
		var uv = new Vector2[4];

		uv[0] = new Vector2(0f, 0f);
		uv[1] = new Vector2(1f, 0f);
		uv[2] = new Vector2(0f, 1f);
		uv[3] = new Vector2(1f, 1f);

		return uv;
	}

	private Vector3[] CreateQuadNormals()
	{
		var normals = new Vector3[4];

		normals[0] = -Vector3.forward;
		normals[1] = -Vector3.forward;
		normals[2] = -Vector3.forward;
		normals[3] = -Vector3.forward;

		return normals;
	}

	private int[] CreateQuadTriangles()
	{
		var triangles = new int[6];

		triangles[0] = 0;
		triangles[1] = 2;
		triangles[2] = 1;

		triangles[3] = 2;
		triangles[4] = 3;
		triangles[5] = 1;

		return triangles;
	}

	private Vector3[] CreateQuadVertexArray(float width, float height)
	{
		var vertices = new Vector3[4];

		vertices[0] = new Vector3(0f, 0f, 0f);
		vertices[1] = new Vector3(width, 0f, 0f);
		vertices[2] = new Vector3(0f, height, 0f);
		vertices[3] = new Vector3(width, height, 0f);

		return vertices;
	}
}
