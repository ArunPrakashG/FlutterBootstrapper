namespace FlutterBootstrapper.Utilities.Attributes {
	[AttributeUsage(AttributeTargets.All)]
	public class StringValueAttribute : Attribute {
		public readonly string Value;

		public StringValueAttribute(string value) => Value = value;
	}
}
