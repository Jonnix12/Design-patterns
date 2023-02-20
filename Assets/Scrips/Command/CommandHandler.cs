using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Bord.Command
{
    public class CommandHandler
    {
        private Stack<ICommand> _commandHistory;

        private List<ICommand> _executeLine;

        public CommandHandler()
        {
            _commandHistory = new Stack<ICommand>();
            _executeLine = new List<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            _executeLine.Add(command);
        }

        public void Execute(out bool isScceeded)
        {
            if (_executeLine[0] == null)
            {
                isScceeded = false;
                return;
            }

            var command = _executeLine[0];
            
            command.Execute(out isScceeded);
            
            if (isScceeded)
            {
                _commandHistory.Push(command);
                _executeLine.RemoveAt(0);
            }
        }

        public void ExecuteAll(out bool isScceeded)
        {
            if (_executeLine[0] == null)
            {
                isScceeded = false;
                return;
            }

            foreach (var command in _executeLine)
            {
                command.Execute(out isScceeded);

                if (isScceeded)
                {
                    _commandHistory.Push(command);
                }
                else
                {
                    Debug.Log("Faild To execute command");
                    isScceeded = false;
                    return;
                }
            }

            isScceeded = true;
            _executeLine.Clear();
        }

        public void Undo()
        {
            var command = _commandHistory.Pop();

            if (command == null)
            {
                return;
            }
            
            command.Undo();
        }
        
        public void UndoAll()
        {
            while (_commandHistory.Count > 0)
            {
                if (_commandHistory.Peek() == null) continue;
                
                var command = _commandHistory.Pop();
                
                command.Undo();
            }
        }
    }
}