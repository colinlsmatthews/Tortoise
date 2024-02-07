using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Tortoise
{
    public class TortoiseInfo : GH_AssemblyInfo
    {
        public override string Name => "Tortoise";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "Simplified architectural panelization operations";

        public override Guid Id => new Guid("2b29cd31-185a-4b51-8815-5e294364c767");

        //Return a string identifying you or your company.
        public override string AuthorName => "Colin Matthews";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}