using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

namespace Tortoise.DataTypes
{
    internal class TriStateType : GH_Goo<int>
    {
         // Default constructor
        public TriStateType() 
        {
            Value = -1;
        }

        // Integer overload
        public TriStateType(GH_Integer triStateValue) 
        {
            //Value = NormalizeValue(triStateValue);
            Value = triStateValue.Value;
        }

        // String overload
        public TriStateType(GH_String triStateValue) : this(ParseStringValue(triStateValue)) { }

        // Boolean overload
        public TriStateType(GH_Boolean triStateValue) : this(triStateValue.Value ? new GH_Integer(1) : new GH_Integer(0)) { }

        // Double overload
        public TriStateType(GH_Number triStateValue) : this(triStateValue.Value > 0 ? new GH_Integer(1) : triStateValue.Value == 0 ? new GH_Integer(0) : new GH_Integer(-1)) { }

        // Copy constructor
        public TriStateType(TriStateType triStateSource) : this(new GH_Integer(triStateSource.Value)) { }

        // Ensures value is within [-1, 1] range.
        //private static int NormalizeValue(GH_Integer Val)
        //{
        //    if (Val.Value <= -1) { return -1; }
        //    if (Val.Value >= 1) { return 1; }
        //    return 0;            
        //}

        // Parses a string to a tri-state value.
        private static GH_Integer ParseStringValue(GH_String Val)
        {
            switch (Val.Value.ToUpperInvariant())
            {
                case "TRUE":
                case "T":
                case "YES":
                case "Y":
                    return new GH_Integer(1);
                case "FALSE":
                case "F":
                case "NO":
                case "N":
                    return new GH_Integer(0);
                case "UNKNOWN":
                case "UNSET":
                case "MAYBE":
                case "DUNNO":
                case "?":
                default:
                    return new GH_Integer(-1);
            }
        }

        // Duplication method (technically not a constructor)
        public override IGH_Goo Duplicate()
        {
            return new TriStateType(this);
        }

        // PROPERTY FORMATTERS***********************************
        // Override the Value property inherited from GH_Goo<T> to strip non-sensical states
        public override int Value
        {
            get { return base.Value; }
            set
            {
                if (value < -1) { value = -1; }
                if (value > +1) { value = +1; }
                base.Value = value;
            }
        }

        // TriState instances are always valid
        public override bool IsValid
        {
            get { return true; }
        }

        // Return a string with the name of this Type
        public override string TypeName
        {
            get { return "Tristate"; }
        }

        // Return a string describing what this Type is about
        public override string TypeDescription
        {
            get { return "A TriState Value (True, False, or Unknown"; }
        }

        // Return a string representation of the state (value) of this instance
        public override string ToString()
        {
            if (this.Value == 0) { return "False"; }
            if (this.Value == 1) { return "True"; }
            return "Unknown";
        }

        // SERIALIZATION***********************************
        // Provide (de)serialization so that the data type can be
        // stored as persistent data within a Grasshopper file.
        // **Highly recommended** if possible.

        // Serialize this instance to a Grasshopper writer object
        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            writer.SetInt32("tri", this.Value);
            return true;
        }

        // Deserialize this instance from a Grasshopper reader object
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            this.Value = reader.GetInt32("tri");
            return true;
        }

        // CASTING***********************************
        // There are three casting methods on IGH_Goo:
        // CastFrom() - facilitate conversations between diff. types of data
        // CastTo() - facilitate conversations between diff. types of data
        // ScriptVariable() - create safe instance to be used in C# script component

        // Return the Integer we use to represent the TriState flag
        public override object ScriptVariable()
        {
            return this.Value;
        }

        // This function is called when Grasshopper needs to convert this
        // instance of TriStateType into some other type Q
        public override bool CastTo<Q>(ref Q target)
        {
            // First, see if Q is similar to the Integer primitive
            if (typeof(Q).IsAssignableFrom(typeof(int)))
            {
                object ptr = this.Value;
                target = (Q)ptr;
                return true;
            }

            // Then, see if Q is similar to the GH_Integer type
            if (typeof(Q).IsAssignableFrom(typeof(GH_Integer)))
            {
                object ptr = new GH_Integer(this.Value);
                target = (Q)ptr;
                return true;
            }

            // Then, see if Q is similar to the Boolean primitive
            if (typeof(Q).IsAssignableFrom(typeof(bool)))
            {
                object ptr = this.Value;
                target = (Q)ptr;
                return true;
            }

            // We could choose to also handle casts to Boolean, GH_Boolean,
            // Double and GH_Number, but this is left as an exercise for the reader
            return false;
        }

        // This function is called when Grasshopper needs to convert other 
        // data into TriState type
        public override bool CastFrom(object source)
        {
            // Abort immediate on bogus data
            if (source == null) { return false; }

            // Use the Grasshopper integer converter. By specifying GH_Conversion.Both
            // we will get both exact and fuzzy results. Fuzzy results are useful when
            // you want to convert a double into an integer (for example).
            // You should always try to use the methods availble through GH_Convert
            // as they are extensive and consistent.
            int val;
            if (GH_Convert.ToInt32(source, out val, GH_Conversion.Both))
            {
                this.Value = val;
                return true;
            }

            // If the integer conversion failed, we can still try to parse Strings.
            // If possible, you chould ensure that your data type can 'deserialize'
            // itself from the output of ToString().
            string str = null;
            if (GH_Convert.ToString(source, out str, GH_Conversion.Both))
            {
                switch (str.ToUpperInvariant())
                {
                    case "TRUE":
                    case "T":
                    case "YES":
                    case "Y":
                        this.Value = 1;
                        return true;

                    case "FALSE":
                    case "F":
                    case "NO":
                    case "N":
                        this.Value = 0;
                        return true;

                    case "UNKNOWN":
                    case "UNSET":
                    case "MAYBE":
                    case "DUNNO":
                    case "?":
                        this.Value = -1;
                        return true;
                }
            }
            // We've exhausted all options, signal failure.
            return false;
        }


    }
}
