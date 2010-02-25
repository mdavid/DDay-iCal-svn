using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DDay.iCal.Serialization
{
    public class DataTypeSerializer : ISerializable, IParameterSerializable
    {
        #region Private Fields

        private iCalDataType m_dataType;
        private ISerializationContext m_SerializationContext;

        #endregion

        #region Protected Properties

        protected iCalDataType DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }

        #endregion

        #region Constructors

        public DataTypeSerializer(iCalDataType dataType)
        {
            this.m_SerializationContext = DDay.iCal.Serialization.SerializationContext.Default;
            this.m_dataType = dataType;
        }

        #endregion

        #region ISerializable Members

        virtual public ISerializationContext SerializationContext
        {
            get { return m_SerializationContext; }
            set { m_SerializationContext = value; }
        }

        virtual public void Serialize(Stream stream, Encoding encoding)
        {
            Type type = m_dataType.GetType();
            ISerializable serializer = null;

            Type serializerType = Type.GetType(GetType().Namespace + "." + type.Name + "Serializer", false, true);
            if (serializerType != null)
                serializer = (ISerializable)Activator.CreateInstance(serializerType, new object[] { m_dataType });

            if (serializer == null)
            {
                if (m_dataType is EncodableDataType)
                    serializer = new EncodableDataTypeSerializer(m_dataType as EncodableDataType);
                else serializer = new FieldSerializer(m_dataType);
            }

            if (serializer != null)
            {
                string value = m_dataType.Name;
                                
                if (serializer is IParameterSerializable)
                {
                    IParameterSerializable paramSerializer = (IParameterSerializable)serializer;
                    List<ICalendarParameter> Parameters = paramSerializer.Parameters;
                    if (Parameters.Count > 0)
                    {                        
                        List<string> values = new List<string>();
                        foreach (ICalendarParameter p in Parameters)
                        {
                            ParameterSerializer pSerializer = new ParameterSerializer(p);
                            values.Add(pSerializer.SerializeToString());
                        }

                        value += ";" + string.Join(";", values.ToArray());
                    }
                }

                value += ":";
                value += serializer.SerializeToString();                

                // FIXME: serialize the line.
                /*ISerializable clSerializer = new ContentLineSerializer(value);
                if (clSerializer != null)
                    clSerializer.Serialize(stream, encoding);
                 */
            }
        }

        virtual public string SerializeToString()
        {
            Type type = m_dataType.GetType();
            ISerializable serializer = null;

            Type serializerType = Type.GetType(GetType().Namespace + "." + type.Name + "Serializer", false, true);
            if (serializerType != null)
                serializer = (ISerializable)Activator.CreateInstance(serializerType, new object[] { m_dataType });

            if (serializer == null)
                serializer = new FieldSerializer(m_dataType);

            if (serializer != null)
                return serializer.SerializeToString();
            else return string.Empty;
        }

        #endregion

        #region IParameterSerializable Members

        virtual public List<ICalendarParameter> Parameters
        {
            get
            {
                List<ICalendarParameter> Parameters = new List<ICalendarParameter>();
                foreach (ICalendarParameter p in m_dataType.Parameters)
                {
                    if (!this.DisallowedParameters.Contains(p))
                        Parameters.Add(p);
                }
                return Parameters;
            }
        }

        virtual public List<ICalendarParameter> DisallowedParameters
        {
            get
            {
                return new List<ICalendarParameter>();
            }
        }

        public object Deserialize(Stream stream, Encoding encoding, Type iCalendarType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion        
    }
}
