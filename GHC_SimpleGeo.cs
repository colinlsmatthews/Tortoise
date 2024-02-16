using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Tortoise
{
    public class GHC_SimpleGeo : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public GHC_SimpleGeo()
          : base("Simple Geometry Component", "SimpleGeo",
              "A simple geometry component",
              "Tortoise", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCircleParameter("Circle", "C", "The circle to slice", GH_ParamAccess.item);
            pManager.AddLineParameter("Line", "L", "Slicing line", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddArcParameter("Arc A", "A", "First Split result", GH_ParamAccess.item);
            pManager.AddArcParameter("Arc B", "B", "Second Split result", GH_ParamAccess.item);
            pManager.AddArcParameter("GH Arc A", "A", "First Split result, as a GH type", GH_ParamAccess.item);
            pManager.AddArcParameter("GH Arc B", "B", "Second Split result, as a GH type", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare placeholder variables and assign invalid data.
            // Will cause abort when not supplied with valid data.
            Rhino.Geometry.Circle circle = Rhino.Geometry.Circle.Unset;
            Rhino.Geometry.Line line = Rhino.Geometry.Line.Unset;

            // Retrieve input data
            if (!DA.GetData(0, ref circle)) { return; }
            if (!DA.GetData(1, ref line)) { return; }

            // Project line segment onto circle plane
            line.Transform(Rhino.Geometry.Transform.PlanarProjection(circle.Plane));

            // Test projected segment for validity
            if (line.Length < Rhino.RhinoMath.ZeroTolerance) 
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Line could not be projected onto the Circle plane");
                return;
            }

            // ilve intersections and abort if there are fewer than two intersections
            double t1;
            double t2;
            Rhino.Geometry.Point3d p1;
            Rhino.Geometry.Point3d p2;

            switch (Rhino.Geometry.Intersect.Intersection.LineCircle(line, circle, out t1, out p1, out t2, out p2)) 
            {
                case Rhino.Geometry.Intersect.LineCircleIntersection.None:
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "No intersections were found");
                    return;

                case Rhino.Geometry.Intersect.LineCircleIntersection.Single:
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Only a single intersection was found");
                    return;
            }

            // Create slicing arcs
            double ct;
            circle.ClosestParameter(p1, out ct);

            Rhino.Geometry.Vector3d tan = circle.TangentAt(ct);
            Rhino.Geometry.Arc arcA = new Rhino.Geometry.Arc(p1, tan, p2);
            Rhino.Geometry.Arc arcB = new Rhino.Geometry.Arc(p1, -tan, p2);

            // Assign output arcs
            DA.SetData(0, arcA);
            DA.SetData(1, arcB);
            DA.SetData(2, new Grasshopper.Kernel.Types.GH_Arc(arcA));
            DA.SetData(3, new Grasshopper.Kernel.Types.GH_Arc(arcB));
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
            get { return new Guid("F591B584-E3D1-4209-94C9-EFE28F20B531"); }
        }
    }
}