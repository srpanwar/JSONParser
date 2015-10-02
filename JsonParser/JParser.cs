using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonParser
{
    /// <summary>
    /// 
    /// </summary>
    public interface JToken
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class JParser
    {
        int parseIndex = -1;
        int dataLength = 0;
        string data = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JToken Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            this.parseIndex = 0;
            this.data = data;
            this.dataLength = data.Length;

            //start the parsing
            this.MoveToFirst();
            if (this.parseIndex == -1)
            {
                throw new ArgumentException("string is not a valid json");
            }

            return this.FormToken();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void MoveToFirst()
        {
            for (int i = 0; i < this.dataLength; i++)
            {
                char charI = this.data[i];
                if (charI == '{' || charI == '[')
                {
                    this.parseIndex = i;
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private JToken FormToken()
        {
            if (this.data[this.parseIndex] == '{')
            {
                return this.FormObject();
            }

            if (this.data[this.parseIndex] == '[')
            {
                return this.FormArray();
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private JArray FormArray()
        {
            JArray jArray = new JArray();

            //we are at the start of object
            this.parseIndex++;

            while (true)
            {
                //skip any white space
                this.SkipWhiteSpace();

                //skip command separator
                this.SkipCommaSeparator();

                //skip any white space
                this.SkipWhiteSpace();

                //For empty objects {}
                //check if we have reached the end of the object or else continue
                if (this.IsEndOfArray())
                {
                    break;
                }

                //extract value
                object value = this.FormValue();

                //add the value to the jobject
                jArray.Add(value);

                //skip any white space
                this.SkipWhiteSpace();

                //check if we have reached the end of the object or else continue
                if (this.IsEndOfArray())
                {
                    break;
                }

            }

            return jArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private JObject FormObject()
        {
            JObject jObject = new JObject();

            //we are at the start of object
            this.parseIndex++;

            while (true)
            {
                //skip any white space
                this.SkipWhiteSpace();

                //skip command separator
                this.SkipCommaSeparator();

                //skip any white space
                this.SkipWhiteSpace();

                //get the property
                string property = this.FormString();

                //skip any white space
                this.SkipWhiteSpace();

                //skip colon separator. find the key:value separator
                this.SkipColonSeparator();

                //skip any white space
                this.SkipWhiteSpace();

                //For empty objects {}
                //check if we have reached the end of the object or else continue
                if (this.IsEndOfObject())
                {
                    break;
                }

                //extract value
                object value = this.FormValue();
                
                //add the property and value to the jobject
                jObject.Add(property, value);

                //skip any white space
                this.SkipWhiteSpace();

                //skip command separator
                this.SkipCommaSeparator();

                //skip any white space
                this.SkipWhiteSpace();

                //check if we have reached the end of the object or else continue
                if (this.IsEndOfObject())
                {
                    break;
                }
            }

            return jObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private object FormValue()
        {
            //if value if object
            if (this.data[this.parseIndex] == '{')
            {
                return this.FormObject();
            }

            //if value if object
            if (this.data[this.parseIndex] == '[')
            {
                return this.FormArray();
            }

            //value can be string, int, bool, real. return string
            return this.FormString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string FormString()
        {
            char delimiter = (char)0;

            //find the string terminator character
            char charI = this.data[this.parseIndex];
            if (charI == '\'' || charI == '"') //if (this.IsStringStart(charI))
            {
                this.parseIndex += 1;
                delimiter = charI;
            }

            //extract the property name
            int startIndex = this.parseIndex;
            int i = this.parseIndex;
            for (; i < this.dataLength; i++)
            {
                charI = this.data[i];
                bool isStrEnd = (this.data[i - 1] != '\\') && 
                                (delimiter == charI || (delimiter == 0 && (charI == ',' || charI == '}')));
                if (isStrEnd) //if (this.IsStringEnd(charI, delimiter) && this.data[i - 1] != '\\')
                {
                    if (delimiter == 0)
                    {
                        this.parseIndex = i;
                    }
                    else
                    {
                        this.parseIndex = i + 1;
                    }
                    break;
                }
            }

            return this.data.Substring(startIndex, i - startIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SkipWhiteSpace()
        {
            for (int i = this.parseIndex; i < this.dataLength; i++)
            {
                char charI = this.data[i];
                if (charI != ' ' && charI != '\n' && charI != '\r') //if (!this.IsSpace(charI))
                {
                    this.parseIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SkipColonSeparator()
        {
            if (this.data[this.parseIndex] == ':')
            {
                this.parseIndex += 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SkipCommaSeparator()
        {
            if (this.data[this.parseIndex] == ',')
            {
                this.parseIndex += 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        //public bool IsSpace(char c)
        //{
        //    return c == ' ' || c == '\n' || c == '\r';
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        //public bool IsStringStart(char c)
        //{
        //    return c == '\'' || c == '"';
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        //public bool IsStringEnd(char c, char startDelim = (char)0)
        //{
        //    return (startDelim != 0 && startDelim == c ) ||
        //           (startDelim == 0 && (c == ',' || c == '}'));
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsEndOfObject()
        {
            if (this.data[this.parseIndex] == '}')
            {
                this.parseIndex += 1;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsEndOfArray()
        {
            if (this.data[this.parseIndex] == ']')
            {
                this.parseIndex += 1;
                return true;
            }

            return false;
        }
    }
}
