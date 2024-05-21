using System;
using System.Data;
using Grasshopper.Kernel;

namespace Tortoise.Archive
{
    public class GHC_MyFirstComponent : GH_Component
    {
        public GHC_MyFirstComponent() : base("MyFirst", "MFC", "My first component", "Tortoise", "Util")
        {

        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("String", "S", "String to reverse", GH_ParamAccess.item);
            // throw new NotImplementedException();
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Reversed", "R", "Reversed string", GH_ParamAccess.item);
            // throw new NotImplementedException();
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string data = null;
            if (!DA.GetData(0, ref data)) { return; }

            if (data == null) { return; }
            if (data.Length == 0) { return; }

            char[] chars = data.ToCharArray();
            Array.Reverse(chars);

            DA.SetData(0, new string(chars));

            // throw new NotImplementedException();
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("b163eb33-a344-4892-835b-e48f5e1b5902"); }
        }

        // Hide the component
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.hidden; }
        }
    }
}
