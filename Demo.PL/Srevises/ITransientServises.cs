using System;

namespace Demo.PL.Srevises
{
    public interface ITransientServises

    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
