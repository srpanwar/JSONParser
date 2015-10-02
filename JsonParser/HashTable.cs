using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonParser
{
    public class HashTable
    {
        List<string> keys = new List<string>();

        public List<string> Keys
        {
            get { return keys; }
            private set { keys = value; }
        }
        List<object> values = new List<object>();

        public List<object> Values
        {
            get { return values; }
            private set { values = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key is null");
                }

                int index = this.keys.IndexOf(key);
                if (index != -1)
                {
                    return this.values[index];
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            if (value == null || string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key or value is null");
            }

            int index = this.keys.IndexOf(key);
            if (index != -1)
            {
                this.values[index] = value;
            }
            else
            {
                this.keys.Add(key);
                this.values.Add(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key is null");
            }

            return  this.keys.IndexOf(key) != -1;
        }
    }
}
