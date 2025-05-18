using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using Tortoise.DataTypes;
using Microsoft.CSharp;
using Grasshopper.Kernel.Types;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.

namespace Tortoise
{
    public class GHC_CardinalConstruct : GH_Component
    {
        // Define default north direction
        private static readonly Vector3d DEFAULT_NORTH = new Vector3d(0, 1, 0);
        
        public GHC_CardinalConstruct()
          : base("Construct Cardinal System", "Cardinal",
              "Define cardinal system for project. Input true north and project north. Acceptable inputs include line curves, numbers, and vectors.",
              "Tortoise", "Project")
        {
        }
        
        // Default value wrapper for GH_Vector 
        private static GH_Vector DefaultNorthVector => new GH_Vector(DEFAULT_NORTH);

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("True North", "T", "Input to define true north direction", GH_ParamAccess.item);
            pManager.AddGenericParameter("Project North", "P", "Input to define project north direction", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "Optional name for the cardinal system", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager[1].Optional = true;
            pManager[2].Optional = true;
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
            // Use object type to handle different input types
            object? trueNorthInput = null;
            object? projectNorthInput = null;
            try
            {
                if (!DA.GetData(0, ref trueNorthInput))
                {
                    trueNorthInput = DefaultNorthVector;
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Remark, "Using default value for True North (0,1,0)");
                }
                else if (trueNorthInput == null)
                {
                    trueNorthInput = DefaultNorthVector;
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "True North input was null. Using default value (0,1,0)");
                }

                if (!DA.GetData(1, ref projectNorthInput))
                {
                    projectNorthInput = DefaultNorthVector;
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Remark, "Using default value for Project North (0,1,0)");
                }
                else if (projectNorthInput == null)
                {
                    projectNorthInput = DefaultNorthVector;
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Project North input was null. Using default value (0,1,0)");
                }
            }
            catch (Exception ex)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Error processing input parameters: {ex.Message}");
                return;
            }

            // Set name to null if not provided
            string nameInput = null;

            // Get name input if provided
            DA.GetData(2, ref nameInput);

            // Define datatype input vectors
            GH_Vector? trueNorth = null;
            GH_Vector? projectNorth = null;

            // Process inputs using helper method
            try
            {
                // Use null-forgiving operator since we've validated the inputs won't be null
                trueNorth = ProcessDirectionalInput(trueNorthInput, "True North");
                if (trueNorth == null) return; // Error already reported
                
                projectNorth = ProcessDirectionalInput(projectNorthInput, "Project North");
                if (projectNorth == null) return; // Error already reported
                
                // Validate vectors are not zero length
                if (trueNorth.Value.Length < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "True North vector has zero length");
                    return;
                }
                
                if (projectNorth.Value.Length < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Project North vector has zero length");
                    return;
                }
            }
            catch (Exception ex)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Error processing directional inputs: {ex.Message}");
                return;
            }

            try
            {
                // Create and set a new CardinalSystem instance based on the input
                DA.SetData(0, new CardinalSystem(trueNorth, projectNorth, nameInput ?? string.Empty));
            }
            catch (Exception ex)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Error creating Cardinal System: {ex.Message}");
                return;
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
            get { return new Guid("BEAA7BAE-F4EF-436B-85F9-2FC2918A2748"); }
        }
        
        /// <summary>
        /// Helper method to process directional inputs of different types
        /// </summary>
        /// <param name="input">The input object (vector, number, or curve)</param>
        /// <param name="inputName">Name of the input for error messages</param>
        /// <returns>A GH_Vector representing the direction, or null if processing failed</returns>
        private GH_Vector ProcessDirectionalInput(object input, string inputName)
        {
            try
            {
                // Null check
                if (input == null)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"{inputName} input is null");
                    return null;
                }
                
                string inputTypeString = input.GetType().ToString();
                
                switch (inputTypeString)
                {
                    case "Grasshopper.Kernel.Types.GH_Vector":
                        return new GH_Vector(((dynamic)input).Value);
                        
                    case "Grasshopper.Kernel.Types.GH_Number":
                        try
                        {
                            double val = ((dynamic)input).Value;
                            double rad = RhinoMath.ToRadians(val);
                            return new GH_Vector(new Vector3d(Math.Sin(rad), Math.Cos(rad), 0));
                        }
                        catch (Exception ex)
                        {
                            AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Error processing {inputName} number: {ex.Message}");
                            return null;
                        }
                        
                    case "Grasshopper.Kernel.Types.GH_Curve":
                        try
                        {
                            var curve = ((dynamic)input).Value;
                            
                            if (!curve.IsLinear())
                            {
                                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"{inputName} curve must be linear");
                                return null;
                            }
                            
                            if (curve.PointAtStart.DistanceTo(curve.PointAtEnd) < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance)
                            {
                                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"{inputName} curve length must be greater than zero");
                                return null;
                            }
                            
                            return new GH_Vector(
                                new Vector3d(curve.PointAtEnd.X - curve.PointAtStart.X,
                                curve.PointAtEnd.Y - curve.PointAtStart.Y,
                                0));
                        }
                        catch (Exception ex)
                        {
                            AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Error processing {inputName} curve: {ex.Message}");
                            return null;
                        }
                        
                    default:
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, 
                           $"Unsupported input type for {inputName}: {inputTypeString}. Use Vector, Number, or Curve.");
                       return null;
                }
            }
            catch (Exception ex)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Error processing {inputName}: {ex.Message}");
                return DefaultNorthVector;
            }
        }
    }
}

#pragma warning restore CS8600
#pragma warning restore CS8603
#pragma warning restore CS8604