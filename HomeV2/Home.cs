using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomeV2
{
    public class Home
    {
        public Home()
        {
            
        }

        public Home(string name, Vector3 position)
        {
            Name = name;
            Position = position;
        }

        public string Name { get; set; }

        public Vector3 Position { get; set; }
    }
}
