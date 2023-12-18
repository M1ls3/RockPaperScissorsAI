using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace RockPaperScissorsAI
{
    public enum Option
    {
        [Description("Rock")]
        Rock,
        [Description("Paper")]
        Paper,
        [Description("Scissors")]
        Scissors
    }

    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }
    }

    public class RPS
    {
        public Option option;

        public RPS(Option option)
        {
            this.option = option;
        }

        static public bool Check(Option regularOption, Option optionAI)
        {
            return regularOption == optionAI;
        }

        static public int Compare(RPS playerOption, RPS AiOprion)
        {
            int result = 2;
            switch (playerOption.option)
            {
                case Option.Rock:
                    switch (AiOprion.option)
                    {
                        case Option.Rock: result = 0; break;
                        case Option.Paper: result = -1; break;
                        case Option.Scissors: result = 1; break;
                    }
                    break;
                case Option.Paper:
                    switch (AiOprion.option)
                    {
                        case Option.Rock: result = 1; break;
                        case Option.Paper: result = 0; break;
                        case Option.Scissors: result = -1; break;
                    }
                    break;
                case Option.Scissors:
                    switch (AiOprion.option)
                    {
                        case Option.Rock: result = -1; break;
                        case Option.Paper: result = 1; break;
                        case Option.Scissors: result = 0; break;    
                    }
                    break;
                default:
                    {
                        Exception exception = new Exception();
                        throw exception;
                    }
            }
            return result;
        }
    }
}
