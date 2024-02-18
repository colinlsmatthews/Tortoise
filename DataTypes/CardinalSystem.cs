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
        // Properties
        public Vector2d TrueNorth { get; set; }
        public Vector2d ProjectNorth { get; set; }
        
        // Default constructor
        public CardinalSystem()
        {
            TrueNorth = new Vector2d(0, 1);
            ProjectNorth = new Vector2d(0, 1);
        }

        // Vector overload
        public CardinalSystem(GH_Vector trueInput, GH_Vector projectInput)
        {
            TrueNorth = new Vector2d(trueInput.Value.X, trueInput.Value.Y);
            ProjectNorth = new Vector2d(projectInput.Value.X, projectInput.Value.Y);
        }

        // Copy constructor
        public CardinalSystem(CardinalSystem source)
        {
            TrueNorth = source.TrueNorth;
            ProjectNorth = source.ProjectNorth;
        }

        // Duplication method (technically not a constructor)
        public override IGH_Goo Duplicate()
        {
            return new CardinalSystem(this);
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
            double tn = Math.Round(DirectionDegrees(TrueNorth), 3);
            string TN = tn.ToString();
            double pn = Math.Round(DirectionDegrees(ProjectNorth), 3);
            string PN = pn.ToString();
            return $"CardinalSystem_TrueNorth:{TN}°_ProjectNorth:{PN}°";
        }
    }
}
