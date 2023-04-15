using UnityEngine;

namespace Flowfield.Demo
{
    public class OrientateToFlow : MonoBehaviour
    {
        [SerializeField]
        private FlowMap _flowMap;

        [SerializeField]
        private int _index1;
        [SerializeField]
        private int _index2;

        private void Awake() => _flowMap = GetComponentInParent<FlowMap>();

        public void Set(int index, int targetIndex)
        {
            _index1 = index;
            _index2 = targetIndex;

            gameObject.SetActive(_index1 > -1 && _index2 > -1);

            if (_index1 < 0 || _index2 < 0)
                return;

            _flowMap.Graph.GetColumnRow(index, out int q, out int r);
            _flowMap.Graph.GetColumnRow(targetIndex, out int q2, out int r2);
            if (_flowMap.Graph.TryGet(q, r, out var n1) && _flowMap.Graph.TryGet(q2, r2, out var n2))
            {
                var p1 = _flowMap.ToWorldCenter(n1.X, n1.Y);
                var p2 = _flowMap.ToWorldCenter(n2.X, n2.Y);

                transform.position = p1;

                var dir = (p1 - p2).normalized;
                transform.forward = dir;
                gameObject.SetActive(true);
            }
            else
                gameObject.SetActive(false);
        }
    }
}