package md53bf72cd20222b62faf818068e8bcf947;


public class ManagedSpanSizeLookup
	extends android.support.v7.widget.GridLayoutManager.SpanSizeLookup
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getSpanSize:(I)I:GetGetSpanSize_IHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.DataControlsRenderer.Android.ManagedSpanSizeLookup, Telerik.XamarinForms.DataControls, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", ManagedSpanSizeLookup.class, __md_methods);
	}


	public ManagedSpanSizeLookup ()
	{
		super ();
		if (getClass () == ManagedSpanSizeLookup.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.DataControlsRenderer.Android.ManagedSpanSizeLookup, Telerik.XamarinForms.DataControls, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ManagedSpanSizeLookup (com.telerik.widget.list.RadListView p0, android.support.v7.widget.GridLayoutManager p1)
	{
		super ();
		if (getClass () == ManagedSpanSizeLookup.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.DataControlsRenderer.Android.ManagedSpanSizeLookup, Telerik.XamarinForms.DataControls, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Com.Telerik.Widget.List.RadListView, Telerik.Xamarin.Android.List, Version=2018.1.202.0, Culture=neutral, PublicKeyToken=null:Android.Support.V7.Widget.GridLayoutManager, Xamarin.Android.Support.v7.RecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
	}


	public int getSpanSize (int p0)
	{
		return n_getSpanSize (p0);
	}

	private native int n_getSpanSize (int p0);

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
