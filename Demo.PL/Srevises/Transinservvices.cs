using System;

namespace Demo.PL.Srevises
{
    public class Transinservvices : ITransientServises
    {
        public Guid Guid { get; set; }

        public Transinservvices() { 
        
        Guid = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return Guid.ToString();
        }
        public override string ToString() {

            return Guid.ToString();
        }
    }
}
