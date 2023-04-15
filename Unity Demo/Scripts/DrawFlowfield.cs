using UnityEngine;

namespace Flowfield.Demo
{
    public class DrawFlowfield : MonoBehaviour
    {
        static Material lineMaterial;

        SeekFlow _seekFlow;
        FlowMap _map;
        [SerializeField]
        GameObject _tilePrefab;
        [SerializeField]
        Transform _tileContainer;
        [SerializeField]
        private Color _obstacleColor;

        void Awake()
        {
            _map = GetComponentInParent<FlowMap>();
            _seekFlow = GetComponentInParent<SeekFlow>();
            _seekFlow.OnRefresh += Refresh;
        }
        private void Refresh()
        {
            int count = Mathf.Max(_seekFlow.FlowField.Length, _tileContainer.childCount);
            for (int i = 0; i < count; i++)
            {
                if (i > _seekFlow.FlowField.Length && i < _tileContainer.childCount)
                {
                    Destroy(_tileContainer.GetChild(i).gameObject);
                    continue;
                }

                OrientateToFlow comp = i >= _tileContainer.childCount ? CreateChild().GetComponent<OrientateToFlow>() : _tileContainer.GetChild(i).GetComponent<OrientateToFlow>();
                comp.gameObject.SetActive(_seekFlow.FoundFlow);
                if (_seekFlow.FoundFlow)
                {
                    int index1 = i;
                    int index2 = _seekFlow.FlowField[i];
                    comp.Set(index1, index2);
                }
            }
        }

        private GameObject CreateChild() => Instantiate(_tilePrefab, _tileContainer);

        static void CreateLineMaterial()
        {
            if (!lineMaterial)
            {
                // Unity has a built-in shader that is useful for drawing
                // simple colored things.
                Shader shader = Shader.Find("Hidden/Internal-Colored");
                lineMaterial = new Material(shader);
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                // Turn on alpha blending
                lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                // Turn backface culling off
                lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                // Turn off depth writes
                lineMaterial.SetInt("_ZWrite", 0);
            }
        }

        // Will be called after all regular rendering is done
        public void OnRenderObject()
        {
            CreateLineMaterial();
            lineMaterial.SetPass(0);

            GL.MultMatrix(transform.localToWorldMatrix);

            DrawObstacles();
        }

        private void DrawObstacles()
        {
            GL.Begin(GL.TRIANGLES);
            GL.Color(_obstacleColor);
            foreach (var obstacle in _map.Obstacles)
            {
                GL.Vertex3(obstacle.x, 0, obstacle.y);
                GL.Vertex3(obstacle.x, 0, obstacle.y + 1);
                GL.Vertex3(obstacle.x + 1, 0, obstacle.y + 1);
                GL.Vertex3(obstacle.x, 0, obstacle.y);
                GL.Vertex3(obstacle.x + 1, 0, obstacle.y + 1);
                GL.Vertex3(obstacle.x + 1, 0, obstacle.y);
            }

            GL.End();
        }
    }
}