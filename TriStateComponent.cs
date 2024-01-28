using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using EnneadTabForGH.DataTypes;

namespace EnneadTabForGH
{
    public class TriStateComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public TriStateComponent()
          : base("TriState Conversion", "TriState",
              "Converts strings and numbers to a tri-state value: \"True\",\"False\", or \"Unknown\"",
              "Ennead", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Input", "I", "Input to convert to a tri-state value", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("TriState", "T", "TriState value of the input");
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare placeholder variable and assign invalid data.
            // Will cause abort when not supplied with valid data.
            var input = object.Unset;

            // Retrieve input data
            if (!DA.GetData(0, ref input)) { return; }
            if (typeof(input) != typeof(string) || 
                typeof(input) != typeof(int) || 
                typeof(input) != typeof(double) ||
                typeof(input) != typeof(bool))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Input must be a string, integer, or double");
                return;
            }

            // Create a new TriStateType instance
            TriStateType tri = new TriStateType(input))

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
            get { return new Guid("EAD2798E-1C22-41C7-8EE0-5DA1FC50FC61"); }
        }
    }
}