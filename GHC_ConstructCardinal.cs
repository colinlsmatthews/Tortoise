using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using Tortoise.DataTypes;
using Microsoft.CSharp;
using Grasshopper.Kernel.Types;

namespace Tortoise
{
    public class GHC_ConstructCardinal : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHC_DefineNorth class.
        /// </summary>
        public GHC_ConstructCardinal()
          : base("Construct Cardinal System", "Cardinal",
              "Define cardinal system for project. Input true north and project north. Acceptable inputs include line curves, numbers, and vectors.",
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
            pManager[0].Optional = true;
            pManager[1].Optional = true;
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
            // Use dynamic type to handle different input types
            dynamic trueNorthInput = null;
            dynamic projectNorthInput = null;
            if (!DA.GetData(0, ref trueNorthInput))
            {
                Vector3d truePlaceholder = new Vector3d(0, 1, 0);
                trueNorthInput = new GH_Vector(truePlaceholder);
            }
            if (!DA.GetData(1, ref projectNorthInput))
            {
                Vector3d projectPlaceholder = new Vector3d(0, 1, 0);
                projectNorthInput = new GH_Vector(projectPlaceholder);
            }

            // Define datatype input vectors
            GH_Vector trueNorth = new GH_Vector();
            GH_Vector projectNorth = new GH_Vector();

            // Determine the input type and convert to GH_Vector
            string trueNorthInputTypeString = trueNorthInput.GetType().ToString();
            string projectNorthInputTypeString = projectNorthInput.GetType().ToString();
            switch (trueNorthInputTypeString)
            {
                case "Grasshopper.Kernel.Types.GH_Vector":
                    trueNorth = new GH_Vector(trueNorthInput);
                    break;
                case "Grasshopper.Kernel.Types.GH_Number":
                    double trueVal = trueNorthInput.Value;
                    double trueRad = RhinoMath.ToRadians(trueVal);
                    trueNorth = new GH_Vector(new Vector3d(Math.Sin(trueRad), Math.Cos(trueRad), 0));
                    break;
                case "Grasshopper.Kernel.Types.GH_Curve":
                    if (!trueNorthInput.Value.IsLinear())
                    {
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Input curve must be linear");
                        return;
                    }
                    if (trueNorthInput.Value.PointAtStart.DistanceTo(trueNorthInput.Value.PointAtEnd) < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance)
                    {
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Curve length must be greater than zero");
                        return;
                    }
                    trueNorth = new GH_Vector(
                        new Vector3d(trueNorthInput.Value.PointAtEnd.X - trueNorthInput.Value.PointAtStart.X, 
                        trueNorthInput.Value.PointAtEnd.Y - trueNorthInput.Value.PointAtStart.Y, 
                        0));
                    break;
            }
            switch (projectNorthInputTypeString)
            {
                case "Grasshopper.Kernel.Types.GH_Vector":
                    projectNorth = new GH_Vector(projectNorthInput);
                    break;
                case "Grasshopper.Kernel.Types.GH_Number":
                    double projectVal = projectNorthInput.Value;
                    double projectRad = RhinoMath.ToRadians(projectVal);
                    projectNorth = new GH_Vector(new Vector3d(Math.Sin(projectRad), Math.Cos(projectRad), 0));
                    break;
                case "Grasshopper.Kernel.Types.GH_Curve":
                    if (!projectNorthInput.Value.IsLinear())
                    {
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Input curve must be linear");
                        return; 
                    }
                    if (projectNorthInput.Value.PointAtStart.DistanceTo(projectNorthInput.Value.PointAtEnd) < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance)
                    {
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Curve length must be greater than zero");
                        return;
                    }
                    projectNorth = new GH_Vector(
                        new Vector3d(projectNorthInput.Value.PointAtEnd.X - projectNorthInput.Value.PointAtStart.X,
                        projectNorthInput.Value.PointAtEnd.Y - projectNorthInput.Value.PointAtStart.Y,
                        0));
                    break;
            }

            // Create and set a new CardinalSystem instance based on the input
            DA.SetData(0, new CardinalSystem(trueNorth, projectNorth));
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