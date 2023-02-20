using System.Collections.Generic;

namespace MoonActive.SequenceSystem
{
    public class SequenceHandler<T>
    {
        /*
         * It's a class that other class can Register to in order to get data,
         * in our case I pass the IGameManager and thus players and if we need other objects could access the data.
         */
        private List<ISequenceOperation<T>> _operations;

        public SequenceHandler()
        {
            _operations = new List<ISequenceOperation<T>>();
        }

        public void Register(ISequenceOperation<T> operation)
        {
            if (_operations.Contains(operation))
                return;
            
            _operations.Add(operation);
        }

        public void StartAll(T data)
        {
            foreach (var operation in _operations)
            {
                operation.ExecuteTask(data);
            }
        }

        public void Reset()
        {
            _operations.Clear();
        }
    }
}