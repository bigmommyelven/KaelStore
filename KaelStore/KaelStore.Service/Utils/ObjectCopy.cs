using System.Linq;

namespace KaelStore.Service.Utils
{
    public static class ObjectCopy
    {
        public static void CopyProperties(object source, object destination)
        {
            foreach (System.Reflection.PropertyInfo p in destination.GetType().GetProperties().Where(p => p.CanWrite))
            {
                p.SetValue(destination, p.GetValue(source, null), null);
            }
        }
    }
}
