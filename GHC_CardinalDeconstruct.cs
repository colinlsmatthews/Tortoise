using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Tortoise.DataTypes;

namespace Tortoise
{
    public class GHC_CardinalDeconstruct : GH_Component
    {
        public GHC_CardinalDeconstruct()
          : base("Deconstruct Cardinal System", "Cardinal",
              "Deconstruct a cardinal system into its component vectors",
              "Tortoise", "Project")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Cardinal System", "C", "The cardinal system to deconstruct", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddVectorParameter("True North", "TN", "The true north vector", GH_ParamAccess.item);
            pManager.AddVectorParameter("True East", "TE", "The true east vector", GH_ParamAccess.item);
            pManager.AddVectorParameter("True South", "TS", "The true south vector", GH_ParamAccess.item);
            pManager.AddVectorParameter("True West", "TW", "The true west vector", GH_ParamAccess.item);
            pManager.AddVectorParameter("Project North", "PN", "The project north vector", GH_ParamAccess.item);
            pManager.AddVectorParameter("Project East", "PE", "The project east vector", GH_ParamAccess.item);
            pManager.AddVectorParameter("Project South", "PS", "The project south vector", GH_ParamAccess.item);
            pManager.AddVectorParameter("Project West", "PW", "The project west vector", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "The name of the cardinal system", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            CardinalSystem cardinalSystem = null;

            if (!DA.GetData(0, ref cardinalSystem)) { return; }

            DA.SetData(0, cardinalSystem.TrueNorth);
            DA.SetData(1, cardinalSystem.TrueEast);
            DA.SetData(2, cardinalSystem.TrueSouth);
            DA.SetData(3, cardinalSystem.TrueWest);
            DA.SetData(4, cardinalSystem.ProjectNorth);
            DA.SetData(5, cardinalSystem.ProjectEast);
            DA.SetData(6, cardinalSystem.ProjectSouth);
            DA.SetData(7, cardinalSystem.ProjectWest);
            DA.SetData(8, cardinalSystem.Name);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("471F8043-253D-4BA5-99B6-A380A97DA075"); }
        }
    }
}