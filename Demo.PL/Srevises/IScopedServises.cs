using System;

namespace Demo.PL.Srevises
{
    public interface IScopedServises
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
