using System;

namespace Demo.PL.Srevises
{
    public class SelgentonServises : IselgentonServises
    {
        public Guid Guid { get; set; }

        public SelgentonServises()
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
