using Grasshopper.Kernel.Types;
using Grasshopper;
using Rhino.Geometry;
using Rhino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortoise.DataTypes;

namespace Tortoise.DataTypes
{
    internal class Massing : GH_Goo<Brep>
    {
        // Properties
        public CardinalSystem ?Cardinal { get; set; }
        public List<Surface> FacadeSurfaces { get; set; }
        public List<string> ?FacadeOrientations { get; private set; }
        public List<Surface> ?RoofSurfaces { get; set; }
        public Grid Grid { get; set; }
        public List<double> Levels { get; set; }
        public string ?Name { get; set; }

        // BEGIN CONSTRUCTORS
        // Default constructor
        public Massing()
        {
            Cardinal = new CardinalSystem();
            FacadeSurfaces = new List<Surface>();
            Grid = new DataTypes.Grid();
            Levels = new List<double>();
            Name = null;
        }

        // Geomety overload (no name)
        public Massing(List<Surface> inputSurfaces, Grid inputGrid, List<double> inputLevels)
        {
            FacadeSurfaces = inputSurfaces;
            Grid = inputGrid;
            Levels = inputLevels;
            Name = null;
        }

        // Geometry overload (with name)
        public Massing(List<Surface> inputSurfaces, Grid inputGrid, List<double> inputLevels, string inputName)
        {
            FacadeSurfaces = inputSurfaces;
            Grid = inputGrid;
            Levels = inputLevels;
            Name = inputName;
        }

        // Copy constructor
        public Massing(Massing source)
        {
            Cardinal = source.Cardinal;
            FacadeSurfaces = source.FacadeSurfaces;
            FacadeOrientations = source.FacadeOrientations;
            RoofSurfaces = source.RoofSurfaces;
            Grid = source.Grid;
            Levels = source.Levels;
            Name = source.Name;
        }

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
