using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using Rhino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortoise.DataTypes
{
    internal class CardinalSystem : GH_Goo<Vector2d>
    {
        // Default constructor
        public CardinalSystem()
        {
            Value = new Vector2d(0, 1);
        }

        // Number overload
        //public CardinalSystem(GH_Number input)
        //{
        //    double inputVal = input.Value;
        //    double inputRad = Rhino.RhinoMath.ToRadians(inputVal);
        //}

        // Copy constructor
        public CardinalSystem(CardinalSystem source)
        {
            Value = source.Value;
        }

        // Duplication method (technically not a constructor)
        public override IGH_Goo Duplicate()
        {
            return new CardinalSystem(this);
        }


        // FORMATTERS
        // CardinalSystem instances are always valid.
        public override bool IsValid
        {
            get { return true; }
        }

        public override string TypeName
        {
            get { return "Cardinal System"; }
        }

        public override string TypeDescription
        {
            get { return "A project cardinal system"; }
        }

        public override string ToString()
        {
            return "Cardinal System";
        }
    }
}
