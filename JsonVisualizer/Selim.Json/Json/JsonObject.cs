﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selim.Json
{
    public class JsonObject : JsonValue
    {

        List<NameValue> nameValues = new List<NameValue>();

        public JsonObject(){}

        public JsonObject(params NameValue[] pairs)
        {
            this.nameValues.AddRange(pairs);
        }

		public JsonObject add(params NameValue[] pairs)
        {
            this.nameValues.AddRange(pairs);
			return this;
        }

		public JsonObject add(NameValue pair)
        {
            this.nameValues.Add(pair);
			return this;
        }

		public JsonObject add(string name, JsonValue value)
        {
			getOrNew(name).value = value;
			return this;
        }

		public JsonObject add(string name, string value)
        {
			var nv = getOrNew(name);

            if (value != null)
            {
                nv.value = new JsonString(value);
            }
            else
            {
                nv.value = new JsonNull();
            }
			return this;
        }

		public JsonObject add(string name, double value)
        {
			getOrNew(name).value =new JsonNumber(value);
			return this;
        }

		public JsonObject add(string name, bool value)
        {
			getOrNew(name).value= new JsonBoolean(value);
			return this;
        }

		/// <summary>
		/// gets the name-value pair with this name if exists, or adds a new one and return it
		/// </summary>
		NameValue getOrNew(string name)
		{
			NameValue nv = nameValues.FirstOrDefault(n => n.name == name);
			if (nv == null)
			{
				nv = new NameValue(name);
				nameValues.Add(nv);
			}
			return nv;
		}

        public int Length { get { return nameValues.Count; } }

        public NameValue[] getNameVlaues()
        {
            return nameValues.ToArray();
        }

        public JsonValue getValue(int index)
        {
            return nameValues.ElementAt(index).value;
        }

        public JsonValue getValue(string name)
        {
            return nameValues.First(nv => nv.name == name).value;
        }

        public string getString(string name)
        {
            JsonString tmp = (JsonString)getValue(name);
            return tmp.value;
        }

        public double getDouble(string name)
        {
            JsonNumber tmp = (JsonNumber)getValue(name);
            return tmp.value;
        }

        public int getInt(string name)
        {
            JsonNumber tmp = (JsonNumber)getValue(name);
            return (int)tmp.value;
        }

        public bool getBool(string name)
        {
            JsonBoolean tmp = (JsonBoolean)getValue(name);
            return tmp.value;
        }

		public JsonObject getObject(string name)
		{
			return (JsonObject)getValue(name);
		}

		public JsonArray getArray(string name)
		{
			return (JsonArray)getValue(name);
		}

        

		public override void render(StringBuilder sb, int? indents)
        {
            
            if (indents != null)
            {
                //appendMany(sb, indentation, (int)indents); 
                sb.Append("{");sb.Append("\r\n");
                
                for (int i = 0; i < this.nameValues.Count; i++ )
                {
                    appendMany(sb, indentation, (int)indents + 1); 
                    nameValues[i].render(sb, indents+1);
                    if (i < nameValues.Count - 1)
                    {
                        sb.AppendLine(",");
                    }
                    else
                    {
                        sb.AppendLine();
                    }
                }
                appendMany(sb, indentation, (int)indents);
                sb.Append("}");
            }
            else
            {
                sb.Append("{");
                foreach (NameValue item in this.nameValues)
                {
                    item.render(sb); sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);//remove the last comma
                sb.Append("}");
            }
        }

	}


    public class NameValue
    {
        public string name;
        public JsonValue value;

		public NameValue(string name)
		{
			this.name = name;
		}

        public NameValue(string name, JsonValue value)
        {
            this.name = name;
            this.value = value;
        }

        public NameValue(string name, string value)
        {
            this.name = name;
            if (value != null)
            {
                this.value = new JsonString(value);
            }
            else
            {
                this.value = new JsonNull();
            }
        }

        public NameValue(string name, bool value)
        {
            this.name = name;
            this.value = new JsonBoolean(value);
        }

        public NameValue(string name, double value)
        {
            this.name = name;
            this.value = new JsonNumber(value);
        }


		internal void render(StringBuilder sb, int? indents= null)
        {
			sb.AppendFormat("\"{0}\":", name);
			value.render(sb, indents);
		}


        private void appendMany(StringBuilder sb, string s, int times)
        {
            for (int i = 0; i < times; i++)
            {
                sb.Append(s);
            }
        }

	}
}
