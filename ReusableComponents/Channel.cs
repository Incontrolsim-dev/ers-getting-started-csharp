using System;
using System.Runtime.InteropServices;
using Ers;

namespace ReusableComponents
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Channel : IDataComponent
    {
        public Entity FromEntity;
        public Entity ToEntity;

        public bool InputOpen = true;
        public bool OutputOpen = true;

        public Channel()
        {
        }
    }
}
