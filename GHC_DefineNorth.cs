using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Tortoise
{
    public class GHC_DefineNorth : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_DefineNorth class.
        /// </summary>
        public GHC_DefineNorth()
          : base("DefineNorth", "North",
              "Define true north and project north. Acceptable inputs include line curves, numbers, and vectors.",
              "Tortoise", "Project")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("True North", "T", "Input to define true north direction", GH_ParamAccess.item);
            pManager.AddGenericParameter("Project North", "P", "Input to define project north direction", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Cardinal System", "C", "The cardinal system of the project", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            DA.SetData(0, new DataTypes.CardinalSystem());
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("D2729F1B-49DF-49A5-ACA8-60CC5F46F4D8"); }
        }
    }
}