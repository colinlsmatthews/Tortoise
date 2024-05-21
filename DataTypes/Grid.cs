using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Rhino;
using Rhino.Geometry;
using Grasshopper.Getters;

namespace Tortoise.DataTypes
{
    internal class Grid : GH_Goo<GH_Structure<IGH_Goo>>
    {
        // PROPERTIES**************************************************
        public GH_Structure<GH_Curve> GridCurves { get; set; }
        public GH_Structure<GH_String> GridNames { get; set; }
        public GH_Structure<GH_Boolean> Flip { get; set; }


        // CONSTRUCTORS************************************************
        // Default constructor
        public Grid()
        {
            GridCurves = new GH_Structure<GH_Curve>();
            GridNames = new GH_Structure<GH_String>();
            Flip = new GH_Structure<GH_Boolean>();
        }

        // Standard constructor
        public Grid(
            GH_Structure<GH_Curve> inputCurves,
            GH_Structure<GH_String> inputNames,
            GH_Structure<GH_Boolean> inputFlip)
        {
            GridCurves = inputCurves;
            GridNames = inputNames;
            Flip = inputFlip;
        }

        // Nameless constructor w/ flip
        public Grid(
            GH_Structure<GH_Curve> inputCurves,
            GH_Structure<GH_Boolean> inputFlip)
        {
            GridCurves = inputCurves;
            GridNames = autoName(); // Add logic to auto name
            Flip = inputFlip;
        }

        // Nameless constructor w/out flip
        public Grid(
            GH_Structure<GH_Curve> inputCurves)
        {
            GridCurves = inputCurves;
            GridNames = autoName();
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

        // MEMBER OVERRIDES********************************************
        // Override properties inherited from IGH_Goo here
        public override GH_Structure<IGH_Goo> Value
        {
            get { return base.Value; }
            set
            {
                GH_Structure<IGH_Goo> dataTree = new GH_Structure<IGH_Goo>();
            }
        }

        public override string ToString()
        {
            return
                $"Structural grid with {numOrientations()} orientations." +
                $"\nOrientation";
                  
            // Structural grid with {numOrientations()} orientations.
            // Orientation {{0;0}[0].name}-{{0;0}[-1].name} (i.e. A-F) has {n} grid lines.
            // Orientaiton {{0;1}[0].name}-{{0;0}[-1].name} (i.e. 1-9) has {n} grid lines.
            // etc...
            // Total number of grid lines: {linesTotal}
            // Total number of structural bays: {baysTotal}
            // Total number of grid intersections: {intersections}
            // 
        }

        public override bool IsValid
        {
            get { return !GridCurves.IsEmpty; }
        }

        public override string TypeName
        {
            get { return "Grid"; }
        }
        public override string TypeDescription
        {
            get { return "Structural grid object"; }
        }

        // METHODS*****************************************************
        private int numOrientations()
        {
            // Calculate number of orientations (num of paths)
            return GridCurves.Branches.Count;
        }

        private int numLines()
        {
            // Calculate number of structural lines
            List<GH_Curve> flattenedData = GridCurves.FlattenData();
            return flattenedData.Count;
            return 0;
        }

        private int numBays()
        {
            // Calculate number of strutural bays
            return 0;
        }

        private int numIntersections()
        {
            // Calculate number of grid intersections
            return 0;
        }

        private GH_Structure<GH_String> autoName()
        {
            // Auto generate grid names
            // Deconstruct GridCurves paths
            // {0;0} - A,B,C,etc...
            // {0;1} - 1,2,3,etc...
            // {0;2} - AA,BB,CC,etc...
            // {0;3} - 101,102,103,etc...
            return new GH_Structure<GH_String>();
        }




    }
}
