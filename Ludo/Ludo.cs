using System;
using System.Reflection.Emit;
using Ludo.Boards;
using Ludo.Entities;
using Ludo.GameModes;
using Ludo.UserInterfaces;

namespace Ludo
{
    internal static class Ludo
    {
        private static void Main()
        {
            Game.GameInstance();
        }
    }
}