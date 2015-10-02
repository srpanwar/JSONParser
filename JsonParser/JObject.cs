using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonParser
{
    public class JObject : JToken
    {
        HashTable properties = new HashTable();

        /// <summary>
        /// 
        /// </summary>
        public HashTable Properties
        {
            get { return properties; }
            private set { properties = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public object this[string property]
        {
            get
            {
                return this.properties[property];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public object this[int index]
        {
            get
            {
                return this.properties.Values[index];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void Add(string property, object value)
        {
            this.properties.Add(property, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public object GetValue(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException("property is null");
            }

            return this.Properties[property];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetString(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException("property is null");
            }

            return this.Properties[property] as string;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public int GetInteger(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException("property is null");
            }

            int result = 0;
            Int32.TryParse(this.Properties[property] as string, out result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public double GetDouble(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException("property is null");
            }

            double result = 0;
            double.TryParse(this.Properties[property] as string, out result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public decimal GetDecimal(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException("property is null");
            }

            decimal result = 0;
            decimal.TryParse(this.Properties[property] as string, out result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool GetBoolean(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException("property is null");
            }

            bool result = false;
            bool.TryParse(this.Properties[property] as string, out result);
            return result;
        }
    }


}
