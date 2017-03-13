using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Free.iso8583.config.attribute
{
    public interface IMask
    {
        IMaskConfig GetConfig(Type modelClass, MethodInfo processDelegate);
        IList<IMask> Children { get; }
    }

    public class Mask : IMask
    {
        public int StartByte { get; set; }
        public byte[] Value { get; set; }
        public byte[] MaskBytes { get; set; }
        public MaskResult Result { get; set; }

        public IMaskConfig GetConfig(Type modelClass, MethodInfo processDelegate)
        {
            string sourceMsg = "Check config for (model=" + modelClass.FullName;
            if (processDelegate != null) sourceMsg += ", process delegate=" + processDelegate.DeclaringType.FullName
                + "." + processDelegate.Name;
            sourceMsg += ")";

            if (StartByte < 0)
                throw new ConfigParserException("StartByte value of Mask class must be non-negative integer value. " + sourceMsg);

            MaskConfig cfg = new MaskConfig();
            cfg.StartByte = StartByte; 
            
            bool isValueExist = Value != null && Value.Length > 0;
            bool isMaskExist = MaskBytes != null && MaskBytes.Length > 0;
            if (!(isValueExist ^ isMaskExist))
            {
                throw new ConfigParserException("Either Value or MaskBytes property must be defined but not both. " + sourceMsg);
            }

            if (isValueExist)
            {
                cfg.Value = Value;
            }
            else
            {
                if (!Enum.GetValues(typeof(MaskResult)).Cast<MaskResult>().Contains(Result) || Result == MaskResult.ValueEquals)
                {
                    throw new ConfigParserException("The Result property must be 'Equals' or 'NotEquals' or 'Zero' or 'NotZero'. "
                        + sourceMsg);
                }
                cfg.Value = MaskBytes;
                cfg.MaskResult = Result.ToString().Substring(0, 1).ToLower() + Result.ToString().Substring(1);
            }

            return cfg;
        }

        public IList<IMask> Children
        {
            get { return null; }
        }
    }

    public enum MaskResult
    {
        ValueEquals = 0,
        Equals = 1,
        NotEquals = 2,
        Zero = 3,
        NotZero = 4
    }


    public class MaskOr : IMask
    {
        public IMaskConfig GetConfig(Type modelClass, MethodInfo processDelegate)
        {
            MaskOrConfig cfg = new MaskOrConfig();
            if (Children != null)
            {
                foreach (IMask mask in Children) cfg.MaskList.Add(mask.GetConfig(modelClass, processDelegate));
            }
            return cfg;
        }

        public IList<IMask> Children { get; set; }
    }


    public class MaskAnd : IMask
    {
        public IMaskConfig GetConfig(Type modelClass, MethodInfo processDelegate)
        {
            MaskAndConfig cfg = new MaskAndConfig();
            if (Children != null)
            {
                foreach (IMask mask in Children) cfg.MaskList.Add(mask.GetConfig(modelClass, processDelegate));
            }
            return cfg;
        }

        public IList<IMask> Children { get; set; }
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MessageToModelAttribute : Attribute
    {
        public Type Model { get; set; }
        public Type ProcessClass { get; set; }
        public string ProcessMethod { get; set; }
    }
}
