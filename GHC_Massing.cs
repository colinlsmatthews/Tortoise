using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Tortoise
{
    public class GHC_Massing : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_Massing class.
        /// </summary>
        public GHC_Massing()
          : base("Define Massing", "Massing", "Massing", "Tortoise", "Project")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Massing", "M", "Massing definition", GH_ParamAccess.item);
            pManager.AddGenericParameter("Cardinal System", "C", "The cardinal system for this massing", GH_ParamAccess.item);
            pManager.AddBrepParameter("Facade Surfaces", "F", "The facade surfaces of this massing", GH_ParamAccess.list);
            pManager.AddGenericParameter("Grid", "G", "The structural grid for this massing", GH_ParamAccess.item);
            pManager.AddGenericParameter("Levels", "L", "The levels for this massing", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "The name of this massing", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager[1].Optional = true;
            pManager[2].Optional = true;
            pManager[3].Optional = true;
            pManager[4].Optional = true;
            pManager[5].Optional = true;
            // Add conditional warning for missing inputs when massing is not provided
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Massing", "M", "Massing definition", GH_ParamAccess.item);
            pManager.AddGenericParameter("Cardinal System", "C", "The cardinal system for this massing", GH_ParamAccess.item);
            pManager.AddBrepParameter("Facade Surfaces", "F", "The facade surfaces of this massing", GH_ParamAccess.list);
            pManager.AddGenericParameter("Grid", "G", "The structural grid for this massing", GH_ParamAccess.item);
            pManager.AddGenericParameter("Levels", "L", "The levels for this massing", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "The name of this massing", GH_ParamAccess.item);
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
            get { return new Guid("33F9586F-BC92-4BBD-91C4-F613D9EFA4D4"); }
        }
    }
}