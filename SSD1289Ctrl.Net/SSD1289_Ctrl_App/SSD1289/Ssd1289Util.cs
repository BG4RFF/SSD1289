﻿using System.Collections.Generic;
using System.IO;

namespace SSD1289_Ctrl_App.SSD1289
{
    struct RegisterValuePair
    {
        public string register;
        public string registerValue;
    }

    class Ssd1289Util
    {
        // Load register,value pairs.
        // File format : 
        // hexadecimal_register_without_0x_prefix,hexadecimal_value_without_0x_prefix
        // or
        // sleep,decimal_value_in_millisec
        public static List<RegisterValuePair> LoadRegisterValue(string fileName)
        {
            List<RegisterValuePair> regValues = new List<RegisterValuePair>();
            char[] delimiters = new char[] { ',' };

            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] elements = line.Split(delimiters);
                    if (elements?.Length == 2)
                    {
                        regValues.Add(new RegisterValuePair() { register = elements[0], registerValue = elements[1] });
                    }
                }
            }

            return regValues;
        }

        public static List<RegisterValuePair> CreateLineWithBlack()
        {
            List<RegisterValuePair> regValues = new List<RegisterValuePair>();
            string reg;
            string regValue;

            for (int i = 0; i < 240; i++)
            {
                // x coordinate.
                reg = string.Format("{0:x4}", 0x004E);
                regValue = string.Format("{0:x4}", i);
                regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });

                // y coordinate.
                reg = string.Format("{0:x4}", 0x004F);
                regValue = string.Format("{0:x4}", i);
                regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });

                // black color.
                reg = string.Format("{0:x4}", 0x0022);
                regValue = string.Format("{0:x4}", 0);
                regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });
            }

            return regValues;
        }

        public static List<RegisterValuePair> CreateBackgroudWithWhite()
        {
            List<RegisterValuePair> regValues = new List<RegisterValuePair>();
            string reg;
            string regValue;

            for (int i = 0; i < 240; i++)
            {
                for (int j = 0; j < 320; j++)
                {
                    // x coordinate.
                    reg = string.Format("{0:x4}", 0x004E);
                    regValue = string.Format("{0:x4}", i);
                    regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });

                    // y coordinate.
                    reg = string.Format("{0:x4}", 0x004F);
                    regValue = string.Format("{0:x4}", j);
                    regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });

                    // white color.
                    reg = string.Format("{0:x4}", 0x0022);
                    regValue = string.Format("{0:x4}", 0xFFFF);
                    regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });
                }
            }

            return regValues;
        }

        public static List<RegisterValuePair> CreateBackgroudWithColor(ushort color)
        {
            List<RegisterValuePair> regValues = new List<RegisterValuePair>();
            string reg;
            string regValue;

            // x coordinate.
            reg = string.Format("{0:x4}", 0x004E);
            regValue = string.Format("{0:x4}", 0);
            regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });

            // y coordinate.
            reg = string.Format("{0:x4}", 0x004F);
            regValue = string.Format("{0:x4}", 0);
            regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });

            for (int i = 0; i < 240; i++)
            {
                for (int j = 0; j < 320; j++)
                {
                    // Color.
                    reg = string.Format("{0:x4}", 0x0022);
                    regValue = string.Format("{0:x4}", color);
                    regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });

                    reg = "sleep";
                    regValue = "10";
                    regValues.Add(new RegisterValuePair() { register = reg, registerValue = regValue });
                }
            }

            return regValues;
        }
    }
}
