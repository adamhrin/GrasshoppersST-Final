package md549dcc44059750c3b8c16eb67d8c50fba;


public class DataFormViewerProvider
	extends md549dcc44059750c3b8c16eb67d8c50fba.DataFormViewProviderBase
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.DataForm.DataFormViewerProvider, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", DataFormViewerProvider.class, __md_methods);
	}


	public DataFormViewerProvider ()
	{
		super ();
		if (getClass () == DataFormViewerProvider.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.DataForm.DataFormViewerProvider, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public DataFormViewerProvider (md5c41373290b44dfab930a7ed997909b14.DataFormRenderer p0, com.telerik.widget.dataform.visualization.RadDataForm p1)
	{
		super ();
		if (getClass () == DataFormViewerProvider.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.DataForm.DataFormViewerProvider, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Telerik.XamarinForms.InputRenderer.Android.DataFormRenderer, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null:Com.Telerik.Widget.Dataform.Visualization.RadDataForm, Telerik.Xamarin.Android.Input, Version=2018.1.202.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
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
