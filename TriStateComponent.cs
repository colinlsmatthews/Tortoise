using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using Tortoise.DataTypes;
using Microsoft.CSharp;

namespace Tortoise
{
    public class TriStateComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public TriStateComponent()
          : base("TriState Conversion", "TriState",
              "Converts strings and numbers to a tri-state value: \"True\",\"False\", or \"Unknown\"",
              "Tortoise", "Util")
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
            pManager.Register_GenericParam("Hard-coded TriState", "TT", "Hard-coded TriState value (should be unknown)");
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Use dynamic type to handle different input types
            dynamic input = null;
            if (!DA.GetData(0, ref input))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "No input provided");
                return;
            }

            // Determine the input type
            string inputTypeString = input.GetType().ToString();
            switch (inputTypeString)
            {
                case "Grasshopper.Kernel.Types.GH_String":
                case "Grasshopper.Kernel.Types.GH_Integer":
                case "Grasshopper.Kernel.Types.GH_Number":
                case "Grasshopper.Kernel.Types.GH_Boolean":
                    // Create and set a new TriStateType instance based on the input
                    TriStateType tri = new TriStateType(input);
                    DA.SetData(0, tri);
                    DA.SetData(1, new TriStateType());
                    break;
                default:
                    // Set error message for unsupported types
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Input type not supported");
                    break;
            }
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