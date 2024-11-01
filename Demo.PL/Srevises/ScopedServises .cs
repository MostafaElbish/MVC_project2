using System;

namespace Demo.PL.Srevises
{
    public class ScopedServises : IScopedServises
    {
        public Guid Guid { get; set; }

        public ScopedServises()
        {

            Guid = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return Guid.ToString();
        }
        public override string ToString()
        {

            return Guid.ToString();
        }
    }
}
