package md51623662fb70fcba808b9d9c078235379;


public class ScrollViewRenderer_LaidOutListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.primitives.panels.ScrollViewLaidOutListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLaidOut:()V:GetOnLaidOutHandler:Com.Telerik.Widget.Primitives.Panels.IScrollViewLaidOutListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.PrimitivesRenderer.Android.ScrollViewRenderer+LaidOutListener, Telerik.XamarinForms.Primitives, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", ScrollViewRenderer_LaidOutListener.class, __md_methods);
	}


	public ScrollViewRenderer_LaidOutListener ()
	{
		super ();
		if (getClass () == ScrollViewRenderer_LaidOutListener.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.PrimitivesRenderer.Android.ScrollViewRenderer+LaidOutListener, Telerik.XamarinForms.Primitives, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onLaidOut ()
	{
		n_onLaidOut ();
	}

	private native void n_onLaidOut ();

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
