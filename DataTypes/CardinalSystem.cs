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
        // BEGIN PROPERTIES
        public Vector2d TrueNorth { get; set; }
        public Vector2d TrueWest { get; set; }
        public Vector2d TrueSouth { get; set; }
        public Vector2d TrueEast { get; set; }
        public Vector2d ProjectNorth { get; set; }
        public Vector2d ProjectWest { get; set; }
        public Vector2d ProjectSouth { get; set; }
        public Vector2d ProjectEast { get; set; }
        public string ?Name { get; set; }
        
        // END PROPERTIES

        // BEGIN CONSTRUCTORS
        // Default constructor
        public CardinalSystem()
        {
            TrueNorth = new Vector2d(0, 1);
            ProjectNorth = new Vector2d(0, 1);
            TrueWest = West(TrueNorth);
            TrueSouth = South(TrueNorth);
            TrueEast = East(TrueNorth);
            ProjectWest = West(ProjectNorth);
            ProjectSouth = South(ProjectNorth);
            ProjectEast = East(ProjectNorth);
            Name = null;
        }

        // Vector overload
        public CardinalSystem(GH_Vector trueInput, GH_Vector projectInput, string nameInput)
        {
            TrueNorth = new Vector2d(trueInput.Value.X, trueInput.Value.Y);
            ProjectNorth = new Vector2d(projectInput.Value.X, projectInput.Value.Y);
            TrueWest = West(TrueNorth);
            TrueSouth = South(TrueNorth);
            TrueEast = East(TrueNorth);
            ProjectWest = West(ProjectNorth);
            ProjectSouth = South(ProjectNorth);
            ProjectEast = East(ProjectNorth);
            Name = nameInput;
        }

        // Copy constructor
        public CardinalSystem(CardinalSystem source)
        {
            TrueNorth = source.TrueNorth;
            ProjectNorth = source.ProjectNorth;
            TrueWest = West(TrueNorth);
            TrueSouth = South(TrueNorth);
            TrueEast = East(TrueNorth);
            ProjectWest = West(ProjectNorth);
            ProjectSouth = South(ProjectNorth);
            ProjectEast = East(ProjectNorth);
            Name = source.Name;
        }

        // END CONSTRUCTORS

        // BEGIN METHODS
        // Duplication method (technically not a constructor)
        public override IGH_Goo Duplicate() => new CardinalSystem(this);

        public Vector2d West(Vector2d north)
        {
            Vector2d west = new Vector2d(north.X, north.Y);
            west.Rotate(-Math.PI * 0.5);
            return west;
        }
        public Vector2d South(Vector2d north)
        {
            Vector2d south = new Vector2d(north.X, north.Y);
            south.Rotate(Math.PI);
            return south;
        }
        public Vector2d East(Vector2d north)
        {
            Vector2d east = new Vector2d(north.X, north.Y);
            east.Rotate(Math.PI * 0.5);
            return east;
        }

        public double DirectionDegrees(Vector2d direction)
        {
            double theta = RhinoMath.ToDegrees(Math.Atan2(direction.X, direction.Y));
            if (theta < 0)
            {
                theta += 360;
            }
            return theta;
        }

        // END METHODS

        // BEGIN FORMATTERS
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
            double tn = Math.Round(DirectionDegrees(TrueNorth), 3);
            string TN = tn.ToString();
            double pn = Math.Round(DirectionDegrees(ProjectNorth), 3);
            string PN = pn.ToString();
            if (Name != null)
            {
                return $"CardinalSystem_\"{Name}\":TrueNorth:{TN}°_ProjectNorth:{PN}°";
            }
            return $"CardinalSystem_TrueNorth:{TN}°_ProjectNorth:{PN}°";
        }

        // END FORMATTERS
    }
}
