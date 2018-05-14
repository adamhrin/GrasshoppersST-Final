package md5d6d37e478b8346feb246f0243ce2fb66;


public class ConvertibleObject_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_equals:(Ljava/lang/Object;)Z:GetEquals_Ljava_lang_Object_Handler\n" +
			"n_hashCode:()I:GetGetHashCodeHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.Common.Android.ConvertibleObject`1, Telerik.XamarinForms.Common, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", ConvertibleObject_1.class, __md_methods);
	}


	public ConvertibleObject_1 ()
	{
		super ();
		if (getClass () == ConvertibleObject_1.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.Common.Android.ConvertibleObject`1, Telerik.XamarinForms.Common, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean equals (java.lang.Object p0)
	{
		return n_equals (p0);
	}

	private native boolean n_equals (java.lang.Object p0);


	public int hashCode ()
	{
		return n_hashCode ();
	}

	private native int n_hashCode ();

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
