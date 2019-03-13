using System;

namespace Ludo.Interfaces
{
    public interface IMenuItem
    {
        void Render();
        void Process(ConsoleKey key);
    }
}