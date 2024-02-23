using System;
using System.Collections.Generic;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using Tortoise.DataTypes;

namespace Tortoise
{
    public class GHC_GridConstruct : GH_Component
    {
        // constructor
        public GHC_GridConstruct()
          : base("Construct Grid", "Grid",
              "Construct a massing grid from curves and text",
              "Tortoise", "Project")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Grid Curves", "C", "Data tree of curves to define grid; each branch is a separate grid direction", GH_ParamAccess.tree);
            pManager.AddTextParameter("Grid Names", "N", "Names of grid lines, in same data structure as curve input", GH_ParamAccess.tree);
            pManager.AddBooleanParameter("Flip", "F", "Flip the grid direction", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Grid", "G", "A grid object, for use with the \"Construct Massing\" component", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Grasshopper.Kernel.Data.GH_Structure<IGH_Goo> gridCurves = null;
            Grasshopper.Kernel.Data.GH_Structure<IGH_Goo> gridNames = null;
            bool flip = false;

            if (!DA.GetDataTree<IGH_Goo>(0, out gridCurves))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "No grid curves input");
                return;
            }
            if (!DA.GetDataTree<IGH_Goo>(1, out gridNames))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "No grid names input");
                return;
            }

            DA.SetData(0, new Grid());
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
            get { return new Guid("B98DDFC5-F713-4A9A-8CAA-C92FF04C992E"); }
        }
    }
}