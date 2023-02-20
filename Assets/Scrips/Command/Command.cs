using System;
using Unity.VisualScripting;

namespace DesignPatterns.Bord.Command
{
    public interface ICommand
    {
        void Execute(out bool isScceeded);
        void Undo();
    }
}