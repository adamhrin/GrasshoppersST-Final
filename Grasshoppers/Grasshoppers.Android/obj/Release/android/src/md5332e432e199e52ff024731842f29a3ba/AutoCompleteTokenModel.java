package md5332e432e199e52ff024731842f29a3ba;


public class AutoCompleteTokenModel
	extends com.telerik.widget.autocomplete.TokenModel
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.AutoComplete.AutoCompleteTokenModel, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", AutoCompleteTokenModel.class, __md_methods);
	}


	public AutoCompleteTokenModel (java.lang.String p0, android.graphics.drawable.Drawable p1)
	{
		super (p0, p1);
		if (getClass () == AutoCompleteTokenModel.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AutoComplete.AutoCompleteTokenModel, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:Android.Graphics.Drawables.Drawable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public AutoCompleteTokenModel (android.os.Parcel p0)
	{
		super (p0);
		if (getClass () == AutoCompleteTokenModel.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AutoComplete.AutoCompleteTokenModel, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Android.OS.Parcel, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
