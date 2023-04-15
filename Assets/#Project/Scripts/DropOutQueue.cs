using System.Collections.Generic;

namespace Rowhouse
{
    public class DropOutQueue<T> : Queue<T>
    {
        private int _max;

        public DropOutQueue(int max) {
            _max = max;
        }

        public new void Enqueue(T value) {
            if (Count < _max) {
                base.Enqueue(value);
            } else {
                Dequeue();
                base.Enqueue(value);
            }
        }
    }
}