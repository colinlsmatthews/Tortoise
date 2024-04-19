using Grasshopper.Kernel.Types;
using Grasshopper;
using Rhino.Geometry;
using Rhino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortoise.DataTypes
{
    internal class Massing : GH_Goo<Brep>
    {
        // Properties
        public Tortoise.DataTypes.CardinalSystem Cardinal { get; set; }
        public List<Surface> FacadeSurfaces { get; set; }
        public List<string> FacadeOrientations { get; set; }
        public List<Surface> RoofSurfaces { get; set; }
        public DataTree<Curve> Grid { get; set; }
        public List<double> Levels { get; set; }
        public string ?Name { get; set; }

        // BEGIN CONSTRUCTORS
        // Default constructor
        public Massing()
        {
            Cardinal = new Tortoise.DataTypes.CardinalSystem();
            FacadeSurfaces = new List<Surface>();
            Grid = new DataTree<Curve>();
            Levels = new List<double>();
            Name = null;
        }

        // Geomety overload (no name)
        public Massing(List<Surface> inputSurfaces, DataTree<Curve> inputGrid, List<double> inputLevels)
        {
            FacadeSurfaces = inputSurfaces;
            Grid = inputGrid;
            Levels = inputLevels;
            Name = null;
        }

        // Geometry overload (with name)


        // END CONSTRUCTORS

        // BEGIN METHODS
        // Duplicate method
        public override IGH_Goo Duplicate() => new Massing(this);

        // END METHODS

        // BEGIN FORMATTERS
        public override bool IsValid
        {
            get { return true; }
        }

        public override string TypeName
        {
            get { return "Massing"; }
        }

        public override string TypeDescription
        {
            get { return "Massing object with facade geometry, grid, and levels"; }
        }

        public override string ToString()
        {
            if (Name != null) { return "Massing object_\"{Name}\""; }
            else { return "Massing object"; }
        }

        // END FORMATTERS
    }

}
