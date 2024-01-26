using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace EnneadTabForGH
{
    public class SimpleMathComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public SimpleMathComponent()
          : base("MyComponent1", "Nickname",
              "Description",
              "Category", "Subcategory")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParamerter("Angle", "A", "The angle to measure", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Radians", "R", "True for radians, False for degrees", GH_ParamAccess.item, true);

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
    }
}