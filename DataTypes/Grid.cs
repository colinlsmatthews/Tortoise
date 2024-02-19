using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino;
using Rhino.Geometry;

namespace Tortoise.DataTypes
{
    internal class Grid : GH_Goo<DataTree<Object>>
    {
        // PROPERTIES
        public DataTree<LineCurve> GridCurves { get; set; }
        public DataTree<string> GridNames { get; set; }
        public bool Flip { get; set; }


        // CONSTRUCTORS
        // Default constructor
        public Grid()
        {
            GridCurves = new DataTree<LineCurve>();
            GridNames = new DataTree<string>();
            Flip = false;
        }

        // Add constructor overloads here
        public Grid(
            DataTree<LineCurve> inputCurves,
            DataTree<string> inputNames,
            bool inputFlip)
        {
            GridCurves = inputCurves;
            GridNames = inputNames;
            Flip = inputFlip;
        }

        // Copy constructor
        public Grid(Grid source)
        {
            GridCurves = source.GridCurves;
            GridNames = source.GridNames;
            Flip = source.Flip;
        }

        // Duplication method (technically not a constructor)
        public override IGH_Goo Duplicate()
        {
            return new Grid(this);
        }

        // MEMBER OVERRIDES
        // Override properties inherited from IGH_Goo here
        public override DataTree<Object> Value
        {
            get { return base.Value; }
            set
            {
                DataTree<Object> dataTree = new DataTree<Object>();

            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool IsValid
        {
            get { return true; }
        }

        public override string TypeName
        {
            get { return "Grid"; }
        }
        public override string TypeDescription
        {
            get { return "Grid"; }
        }


    }
}
