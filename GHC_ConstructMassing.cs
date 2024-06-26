﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Tortoise
{
    public class GHC_ConstructMassing : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ConstructMassingComponent class.
        /// </summary>
        public GHC_ConstructMassing()
          : base("Construct Massing", "Massing",
              "Construct a Massing object from grids, levels, and facade geometry",
              "Tortoise", "Design Options")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // Add input parameters
            pManager.AddGenericParameter("Cardinal System", "C", "The cardinal system for the massing", GH_ParamAccess.item);
            pManager.AddBrepParameter("Facade Surfaces", "F", "The facade surfaces of the massing", GH_ParamAccess.list);
            pManager.AddGenericParameter("Grid", "G", "Structural grid", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
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
            get { return new Guid("86BFD4C9-B607-4B33-A0C6-925CB03DC809"); }
        }
    }
}