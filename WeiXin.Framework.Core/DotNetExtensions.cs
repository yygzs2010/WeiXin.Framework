using System;
using System.Reflection;
using System.Xml.Linq;

namespace WeiXin.Framework.Core
{
    /// <summary>
    /// .net扩展类，扩展相关类的方法
    /// </summary>
    public static class DotNetExtensions
    {
        /// <summary>
        /// 获取时间相对于1970.1.1日的时间戳（整型）
        /// </summary>
        /// <param name="dateTime">时间对象</param>
        /// <returns>时间戳（整型）</returns>
        public static int GetInt(this DateTime dateTime)
        {
            try
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return Convert.ToInt32((DateTime.Now - startTime).TotalSeconds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据相对于1970.1.1日的时间戳（整型）获取时间
        /// </summary>
        /// <param name="intString">时间戳（整型）</param>
        /// <returns>DateTime时间对象</returns>
        public static DateTime IntStringToDateTime(this String intString)
        {
            try
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long longTime = long.Parse(intString + "0000000");
                TimeSpan timeNow = new TimeSpan(longTime);
                return startTime.Add(timeNow);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 通过反射将XElement转换成类型T的实体对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="element">XElement对象</param>
        /// <returns>类型T的对象</returns>
        public static T Get<T>(this XElement element) where T : class
        {
            try
            {
                Type type = typeof(T);
                T entity = Activator.CreateInstance(type) as T;
                PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    XElement temp = element.Element(propertyInfo.Name);
                    if (temp != null)
                    {
                        propertyInfo.SetValue(entity, temp.Value, null);
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 实体对象转换成XElement对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>XElement对象</returns>
        public static XElement GetXElement(this object entity)
        {
            try
            {
                XElement element = new XElement("xml");
                Type type = entity.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    object value = propertyInfo.GetValue(entity, null);
                    if (value == null) continue;

                    XElement temp = new XElement(propertyInfo.Name, value);
                    element.Add(temp);
                }
                //using (MemoryStream memoryStream=new MemoryStream())
                //{
                //    XmlSerializer xmlSerializer=new XmlSerializer(entity.GetType());
                //    xmlSerializer.Serialize(memoryStream,entity);

                //    memoryStream.Position = 0;
                //    using (StreamReader streamReader = new StreamReader(memoryStream))
                //    {
                //        string xml = streamReader.ReadToEnd();
                //        element.Add(XElement.Parse(xml));
                //    }
                //    element.Add(XElement.Load(memoryStream));
                //}
                return element;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
