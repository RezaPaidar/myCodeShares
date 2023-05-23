//Convert List to Datatable
public static DataTable DTDataSource<T>(this IList<T> data)
{
	DataTable dt = new DataTable();
	PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
	for (int i = 0; i < props.Count; i++)
	{
		PropertyDescriptor prop = props[i];
		if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
			dt.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
		else
			dt.Columns.Add(prop.Name, prop.PropertyType);
	}
	object[] values = new object[props.Count];
	foreach (T item in data)
	{
		for (int i = 0; i < values.Length; i++)
		{
			values[i] = props[i].GetValue(item);
		}
		dt.Rows.Add(values);
	}
	return dt;
}
