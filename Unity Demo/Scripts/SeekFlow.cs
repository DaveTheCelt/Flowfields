using System;
using UnityEngine;
namespace Flowfield.Demo
{
    public class SeekFlow : MonoBehaviour
    {
        int[] _flowfield;

        FlowMap _map;
        Vector2Int _target;

        float _delta;
        [SerializeField, Range(0.5f, 10), Header("Recalculate Path")]
        float _rate = 4;

        [SerializeField, Range(0, 100)]
        private int _totalObstacles;
        [SerializeField]
        private bool _regenerateObstacles;
        private bool _foundFlow;

        public Vector3 TargetPoint => _map.ToWorldCenter(_target.x, _target.y);
        public Action OnRefresh { get; set; }
        public bool FoundFlow => _foundFlow;
        public float RecalculateRate => _rate;
        public int[] FlowField => _flowfield;

        void Awake()
        {
            _map = GetComponent<FlowMap>();
            _flowfield = new int[_map.Rows * _map.Columns];
        }

        private void Start() => Refresh();

        private void Update()
        {
            if (_regenerateObstacles)
            {
                _map.GenerateObstacles(_totalObstacles);
                _regenerateObstacles = false;
                Refresh();
            }

            _delta += Time.deltaTime;
            if (_delta >= _rate)
                Refresh();
        }

        void Refresh()
        {
            Array.Clear(_flowfield, 0, _flowfield.Length);
            if (_flowfield.Length != _map.Rows * _map.Columns)
                _flowfield = new int[_map.Rows * _map.Columns];

            _delta = 0;
            GetNextTarget();
            CreateFlowField();
            OnRefresh?.Invoke();
        }
        void CreateFlowField() => _foundFlow = _map.CreateFlowField(_target.x, _target.y, _flowfield, Pathfinding.TravelType.Ordinal);

        void GetNextTarget()
        {
            int x = UnityEngine.Random.Range(0, _map.Columns);
            int y = UnityEngine.Random.Range(0, _map.Rows);
            if (x == _target.x && y == _target.y)
                GetNextTarget();

            _target.x = x;
            _target.y = y;
        }
    }
}