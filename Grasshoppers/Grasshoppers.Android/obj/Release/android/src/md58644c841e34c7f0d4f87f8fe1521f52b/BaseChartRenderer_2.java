package md58644c841e34c7f0d4f87f8fe1521f52b;


public abstract class BaseChartRenderer_2
	extends md5d6d37e478b8346feb246f0243ce2fb66.AndroidRendererBase_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.ChartRenderer.Android.BaseChartRenderer`2, Telerik.XamarinForms.Chart, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", BaseChartRenderer_2.class, __md_methods);
	}


	public BaseChartRenderer_2 (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == BaseChartRenderer_2.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ChartRenderer.Android.BaseChartRenderer`2, Telerik.XamarinForms.Chart, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public BaseChartRenderer_2 (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == BaseChartRenderer_2.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ChartRenderer.Android.BaseChartRenderer`2, Telerik.XamarinForms.Chart, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public BaseChartRenderer_2 (android.content.Context p0)
	{
		super (p0);
		if (getClass () == BaseChartRenderer_2.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ChartRenderer.Android.BaseChartRenderer`2, Telerik.XamarinForms.Chart, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
