using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Tortoise
{
    public class GHC_SimpleMath : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public GHC_SimpleMath()
          : base("Simple Math Component", "SimpleMath",
              "SinCosTan",
              "Tortoise", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Angle", "A", "The angle to measure", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Radians", "R", "Work in Radians", GH_ParamAccess.item, true);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Sin", "S", "The sine of the angle", GH_ParamAccess.item);
            pManager.AddNumberParameter("Cos", "C", "The cosine of the angle", GH_ParamAccess.item);
            pManager.AddNumberParameter("Tan", "T", "The tangent of the angle", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double angle = double.NaN;
            bool radians = false;

            if (!DA.GetData(0, ref angle)) { return; }
            if (!DA.GetData(1, ref radians)) { return; }

            if (!Rhino.RhinoMath.IsValidDouble(angle)) { return; }

            if (radians)
            {
                angle = Rhino.RhinoMath.ToRadians(angle);
            }

            DA.SetData(0, Math.Sin(angle));
            DA.SetData(1, Math.Cos(angle));
            DA.SetData(2, Math.Tan(angle));

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
            get { return new Guid("720E8F20-71EC-4652-A733-65D9EA6964B8"); }
        }

        // Hide the component
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.hidden; }
        }
    }
}