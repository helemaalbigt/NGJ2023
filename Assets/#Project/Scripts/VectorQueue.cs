using UnityEngine;

namespace Rowhouse
{
    public class VectorQueue : DropOutQueue<Vector3>
    {
        public VectorQueue(int max) : base(max) {
        }

        public Vector3 Average() {
            if (Count == 0)
                return Vector3.zero;

            Vector3 sum = Vector3.zero;
            foreach (var val in this)
                sum += val;
            return sum / Count;
        }

        public Vector3 Min(bool excludeZero = false){
            if (Count == 0){
                return Vector3.zero;
            }
            else{
                Vector3 min = Vector3.one * 999999f;
                foreach (var vec in this){
                    if(vec == Vector3.zero)
                        continue;
                    if (vec.sqrMagnitude < min.sqrMagnitude)
                        min = vec;
                }
                return min;
            }
        }
        
        public Vector3 Max(bool excludeZero = false){
            if (Count == 0){
                return Vector3.zero;
            }
            else{
                Vector3 max = -Vector3.one * 999999f;
                foreach (var vec in this){
                    if(vec == Vector3.zero)
                        continue;
                    if (vec.sqrMagnitude > max.sqrMagnitude)
                        max = vec;
                }
                return max;
            }
        }

        public Vector3 AverageDirection() {
            if (Count <= 1)
                return Vector3.zero;

            bool prevSet = false;
            Vector3 prev = Vector3.zero;
            Vector3 avgDir = Vector3.zero;
            foreach (var vec in this) {
                if (prevSet) {
                    avgDir += vec - prev;
                }

                prev = vec;
                prevSet = true;
            }

            avgDir /= Count - 1f;
            return avgDir;
        }
    }
}