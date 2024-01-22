using System;
using Grasshopper.Kernel;

namespace EnneadTabForGH
{
    public class MyFirstComponent : GH_Component
    {
        public MyFirstComponent() : base("MyFirst", "MFC", "My first component", "Extra", "Simple")
        {

        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            throw new NotImplementedException();
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            throw new NotImplementedException();
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            throw new NotImplementedException();
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("b163eb33-a344-4892-835b-e48f5e1b5902"); }
        }
    }
}
